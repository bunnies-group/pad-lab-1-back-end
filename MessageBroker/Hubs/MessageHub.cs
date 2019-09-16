using System;
using System.Threading.Tasks;
using Application.ConnectionService;
using Application.MessageService;
using Microsoft.AspNetCore.SignalR;
using Models.Entities;
using MongoDB.Bson;

namespace MessageBroker.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IConnectionService _connectionService;

        public MessageHub(IMessageService messageService, IConnectionService connectionService)
        {
            _messageService = messageService;
            _connectionService = connectionService;
        }

        public async Task SendMessage(string message, string topicName)
        {
            await _messageService.Create(new Message
            {
                Content = message,
                Topic = topicName
            });

            await Clients.Group(topicName).SendAsync("message", message);
        }

        public async Task Subscribe(string topicName)
        {
            await _connectionService.Create(new Connection
            {
                Topic = topicName,
                ConnectionId = Context.ConnectionId
            });

            await Groups.AddToGroupAsync(Context.ConnectionId, topicName);

            var messages = await _messageService.GetByTopic(topicName);
            await Clients.Caller.SendAsync("connect", messages.ToJson());
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connection = await _connectionService.Get(Context.ConnectionId);

            await _connectionService.Remove(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.Topic);
        }
    }
}