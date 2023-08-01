using Messaging.POCO;
using Messaging.Service;
using Messaging.ServiceHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
    internal class MessageBroker
    {

        public event EventHandler<NotificationEventArgBroker> OnMessage;
        public event EventHandler<MessageAddIntegrationEvent> MessageAddIntegrationEvent;
        IEventBus _eventBus { get; set; }

        public MessageBroker(IEventBus eventBus) {

            _eventBus = eventBus;

        }
        /// <summary>
        /// Subscribe to Message queue form where message would be read
        /// </summary>
        /// <param name="q"></param>
        public void Subscribe(MessageQueue q)
        {
            q.OnMessage += Q_OnMessage;
        }
        /// <summary>
        /// Broker subscribing to event bus as well
        /// </summary>
        /// <param name="eventBus"></param>
        public void Subscribe(IEventBus eventBus)
        {
            eventBus.OnMessageAdd += eventBus_OnMessageAdd;
        }
        /// <summary>
        /// All events generated in event bus would be executed form here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventBus_OnMessageAdd(object sender, MessageAddIntegrationEvent e)
        {
            e.IntegrationHanlder(e.ActionEvent, e.Param,e.CallBack,e.ErrorCallback);
            
        }
        /// <summary>
        /// Event handler message is dequeued
        /// </summary>
        /// <param name="q"></param>
        /// <param name="e"></param>
        private void Q_OnMessage(object q, QueueNotificationEvent e)
        {

            Action<IEventReponse> callbackHandlerGet = (o) =>
            {
                int count = (int)((o as MessageEventResponse).Result);
                OnMessage?.Invoke(this, new NotificationEventArgBroker() { Count = count });
            };

            Action<IEventReponse> callbackHandler = (o) =>
            {
                Guid MsgId = (o as MessageEventResponse).MsgCorelationId;
                var msgdomainget = new MessageEvent() { MsgCorelationId = Guid.NewGuid(), Message = e.Message, ServiceOperation = Constants.GET};
                _eventBus.Trigger(msgdomainget, HandlerFactory.MessageAction, callbackHandlerGet, null);

            };

            var msgdomain = new MessageEvent() { MsgCorelationId = Guid.NewGuid() , Message = e.Message , ServiceOperation = Constants.ADD };
            _eventBus.Trigger(msgdomain, HandlerFactory.MessageAction, callbackHandler, null);

            
            
        }
    }
}
