using System;
using System.Diagnostics.CodeAnalysis;

namespace APIWoood.Logic.SharedKernel.Exceptions
{
    [ExcludeFromCodeCoverage]

    public sealed class MessageBrokerTypeNotSupportedException : Exception
    {
        public MessageBrokerTypeNotSupportedException() { }
        public MessageBrokerTypeNotSupportedException(string message) : base(message) { }
        public MessageBrokerTypeNotSupportedException(string message, Exception inner) : base(message, inner) { }
    }
}
