using Messaging.POCO;
using Messaging.ServiceHandler;
using System;

namespace Messaging
{
    public class QueueNotificationEvent:EventArgs
    {
        public string Message { get; set; }
      
    }

    public class NotificationEventArgBroker : EventArgs
    {
        public int Count { get; set; }

    }

    public class MessageAddIntegrationEvent:EventArgs
    { 
        public IEvent Param { get; set; } 
        public Action<string,IEvent,Action<IEventReponse>, Action<IEventErrorResponse>> IntegrationHanlder { get; set; }
        public string ActionEvent { get; set; }
        public Action<IEventReponse> CallBack { get; set; }
        public Action<IEventErrorResponse> ErrorCallback { get; set; }
    }





}