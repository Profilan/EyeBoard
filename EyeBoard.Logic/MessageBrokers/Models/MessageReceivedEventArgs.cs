using System;
using System.Threading;

namespace EyeBoard.Logic.MessageBrokers.Models
{
    public sealed class MessageReceivedEventArgs : EventArgs
    {
        public Message ReceivedMessage { get; }
        public string AcknowledgeToken { get; }
        public CancellationToken CancellationToken { get; }

        public MessageReceivedEventArgs(Message receivedMessage, string acknowledgeToken, CancellationToken cancellationToken)
        {
            ReceivedMessage = receivedMessage;
            AcknowledgeToken = acknowledgeToken;
            CancellationToken = cancellationToken;
        }
    }
}
