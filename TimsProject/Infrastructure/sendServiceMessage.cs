using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TimsProject.Models;

namespace TimsProject.Infrastructure
{
    public class SendServiceBusMessage
    {
        // connection string to your Service Bus namespace
        private readonly ILogger _logger;

        // name of the Service Bus topic
        public IConfiguration _configuration;

        // the client that owns the connection and can be used to create senders and receivers
        public ServiceBusClient _client;

        // the sender used to publish messages to the topic
        public ServiceBusSender _clientsender;

        public SendServiceBusMessage(IConfiguration _configuration, ILogger<SendServiceBusMessage> logger)
        {
            _logger = logger;
            var _serviceBusConnectionString = _configuration["ServiceBusConnectionString"];
            string _queueName = _configuration["ServiceBusQueueName"];
            _client = new ServiceBusClient(_serviceBusConnectionString);
            _clientsender = _client.CreateSender(_queueName);
        }
        public async Task sendServiceBusMessage(ServiceBusMessageData Message)
        {
            var messagePayLoad = JsonSerializer.Serialize(Message);
            ServiceBusMessage ServiceBusMessageData = new ServiceBusMessage(messagePayLoad);
            try
            {
                await _clientsender.SendMessageAsync(ServiceBusMessageData);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
