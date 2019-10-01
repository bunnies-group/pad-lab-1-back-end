using System;
using System.Threading.Tasks;
using Application.MessageEnricher;
using Application.MessageService;
using Application.SubscriptionService;
using Microsoft.AspNetCore.SignalR;
using Models.Entities;
using MongoDB.Bson;
using Serilog;

namespace MessageBroker.Hubs
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMessageEnricher _messageEnricher;

        public MessageHub(
            IMessageService messageService,
            ISubscriptionService subscriptionService,
            IMessageEnricher messageEnricher
        )
        {
            _messageService = messageService;
            _subscriptionService = subscriptionService;
            _messageEnricher = messageEnricher;
        }

        private const string LogTemplate = "{0} send message \"{1}\" translated into \"{2}\" to topic \"{3}\"";

        public async Task SendMessage(string message, string topicName)
        {
            var translatedMessage = _messageEnricher.Translate(message);

            Log.Information(LogTemplate, Context.ConnectionId, message, translatedMessage, topicName);

            await _messageService.Create(new Message
            {
                Content = translatedMessage,
                Topic = topicName
            });

            await Clients.Group(topicName).SendAsync("message", translatedMessage);
        }

        public async Task Subscribe(string topicName)
        {
            var subscription = await _subscriptionService.Get(Context.ConnectionId);

            if (subscription != null)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, subscription.Topic);
                await _subscriptionService.Remove(Context.ConnectionId);
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