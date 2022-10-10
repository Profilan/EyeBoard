using EyeBoard.Logic.MessageBrokers.Models;
using System;
using System.Threading.Tasks;

namespace EyeBoard.Logic.MessageBrokers.Publishers
{
    public abstract class PublisherBase : IDisposable
    {
        public Task Publish(Message message)
        {
            return PublishCore(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract Task PublishCore(Message message);
        protected abstract void Dispose(bool disposing);
    }
}
