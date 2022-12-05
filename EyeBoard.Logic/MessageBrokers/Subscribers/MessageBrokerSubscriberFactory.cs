using APIWoood.Logic.SharedKernel.Exceptions;
using Profilan.SharedKernel;
using System.Configuration;

namespace EyeBoard.Logic.MessageBrokers.Subscribers
{
    public static class MessageBrokerSubscriberFactory
    {
        public static SubscriberBase Create(MessageBrokerType messageBrokerType)
        {
            SubscriberBase subscriber = null;
            string connectionString = null;

            switch (messageBrokerType)
            {
                case MessageBrokerType.RabbitMq:
                    subscriber = new SubscriberRabbitMq();
                    connectionString = ConfigurationManager.AppSettings["BrokerConnectionString"]; // brokerConnectionStringRabbitMq;
                    break;
                default:
                    throw new MessageBrokerTypeNotSupportedException($"The MessageBrokerType: {messageBrokerType}, is not supported yet");
            }

            var commandTopic = ConfigurationManager.AppSettings["BrokerTopic"];
            var commandQueue = ConfigurationManager.AppSettings["BrokerQueue"];

            subscriber.Initialize(connectionString, commandTopic, commandQueue);
            return subscriber;
        }
    }
}
