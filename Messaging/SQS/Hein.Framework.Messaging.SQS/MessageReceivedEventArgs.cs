using System;

namespace Hein.Framework.Messaging.SQS
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(string message) : base()
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
}
