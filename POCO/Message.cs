using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.POCO
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsProcessed { get; set; }

        public bool IsError { get; set; }
        public string Error { get; set; }
    }

    public class MessageEvent : IEvent
    {
        public Guid MsgCorelationId { get; set; }
        public string Message { get; set; }
        public string ServiceOperation { get; set; }
    }

    public class MessageEventResponse : IEventReponse
    {
        public Guid MsgCorelationId { get; set; }
        public dynamic Result { get; set; }
        public string ServiceOperation { get; set; }
    }

    public class MessageEventErrorReponse : IEventErrorResponse
    {
        public Guid MsgCorelationId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
