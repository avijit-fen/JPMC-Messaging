using Messaging.POCO;
using Messaging.ServiceHandler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
    public class EventBus : IEventBus
    {
        public event EventHandler<MessageAddIntegrationEvent> OnMessageAdd;
        private static EventBus instance = null;
        private static readonly object padlock = new object();

        EventBus() { }

        public static EventBus Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new EventBus();
                    }
                    return instance;
                }
            }
        }
        /// <summary>
        /// Trigerred by Handler to execute event inturn call service or other handler
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="integrationHanlder"></param>
        /// <param name="callback"></param>
        /// <param name="errorCallback"></param>
        public void Trigger(IEvent msg, Action<string,IEvent,Action<IEventReponse>,Action<IEventErrorResponse>> integrationHanlder, Action<IEventReponse> callback , Action<IEventErrorResponse> errorCallback)
        {
            OnMessageAdd?.Invoke(this, new MessageAddIntegrationEvent() { Param = msg , IntegrationHanlder = integrationHanlder , CallBack = callback , ErrorCallback = errorCallback });
            
 
        }


    }
}
