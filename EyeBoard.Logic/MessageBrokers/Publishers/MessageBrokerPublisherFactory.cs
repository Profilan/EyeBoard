using APIWoood.Logic.SharedKernel.Exceptions;
using Profilan.SharedKernel.Enums;
using System.Configuration;

namespace EyeBoard.Logic.MessageBrokers.Publishers
{
    public static class MessageBrokerPublisherFactory
    {
        public static PublisherBase Create(MessageBrokerType messageBrokerType)
        {
            switch (messageBrokerType)
            {
                case MessageBrokerType.RabbitMq:
                    string brokerConnectionString = ConfigurationManager.AppSettings["MessageBrokerConnectionString"];
                    string brokerTopic  = ConfigurationManager.AppSettings["MessageBrokerTopic"];
                    return new PublisherRabbitMq(brokerConnectionString, brokerTopic);
            }

            throw new MessageBrokerTypeNotSupportedException($"The MessageBrokerType: {messageBrokerType}, is not supported yet");
        }
    }
}
