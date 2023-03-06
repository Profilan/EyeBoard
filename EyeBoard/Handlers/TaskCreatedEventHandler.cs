using EyeBoard.Logic.Events;
using EyeBoard.Logic.MessageBrokers.Models;
using EyeBoard.Logic.MessageBrokers.Publishers;
using Profilan.SharedKernel;
using System;
using System.Text;
using System.Text.Json;

namespace EyeBoard.Handlers
{
    public class TaskCreatedEventHandler : IHandle<TaskCreatedEvent>
    {
        private readonly PublisherBase _publisher;

        public TaskCreatedEventHandler() 
        {
            _publisher = MessageBrokerPublisherFactory.Create(Profilan.SharedKernel.Enums.MessageBrokerType.RabbitMq);
        }
        public void Handle(TaskCreatedEvent args)
        {
            var messageId = Guid.NewGuid().ToString("N");

            var taskMessage = new TaskMessage
            {
                TaskId = args.Task.Id,
            };

            var taskMessageJson = JsonSerializer.Serialize(taskMessage);
            var messageBytes = Encoding.UTF8.GetBytes(taskMessageJson);
            var brokerMessage = new Message(messageBytes, messageId, "application/json", DateTime.Now);
            _publisher.Publish(brokerMessage);
        }
    }
}
