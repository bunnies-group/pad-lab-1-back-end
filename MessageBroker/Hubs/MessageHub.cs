using System;
using System.Threading.Tasks;
using Application.MessageService;
using Application.SubscriptionService;
using Microsoft.AspNetCore.SignalR;
using Models.Entities;
using MongoDB.Bson;

namespace MessageBroker.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly ISubscriptionService _subscriptionService;

        public MessageHub(IMessageService messageService, ISubscriptionService subscriptionService)
        {
            _messageService = messageService;
            _subscriptionService = subscriptionService;
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
            var subscription = await _subscriptionService.Get(Context.ConnectionId);

            if (subscription != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, subscription.Topic);
            }

            await _subscriptionService.Create(new Subscription
            {
                Topic = topicName,
                SubscriptionId = Context.ConnectionId
            });

            await Groups.AddToGroupAsync(Context.ConnectionId, topicName);

            var messages = await _messageService.GetByTopic(topicName);
            await Clients.Caller.SendAsync("connect", messages.ToJson());
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var subscription = await _subscriptionService.Get(Context.ConnectionId);

            await _subscriptionService.Remove(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, subscription.Topic);
        }
    }
}