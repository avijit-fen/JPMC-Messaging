using Messaging.POCO;
using Messaging.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.ServiceHandler
{
    /// <summary>
    /// Intermediate Handler to create trade event
    /// </summary>
    public class TradeSaleMsgHandler : IIntegrationHanlder<MessageEvent,MessageEventResponse,MessageEventErrorReponse>
    {
        IEventBus _eventBus { get; set; }
        public TradeSaleMsgHandler(IEventBus eventBus) {

            _eventBus = eventBus;
        }
        public void Execute(MessageEvent args, Action<MessageEventResponse> callback, Action<MessageEventErrorReponse> errorCallBack)
        {
            var helper = new SalesHelper();
            var TradeDomainObject = helper.Parse(args);
            if (TradeDomainObject == null)
            {
                errorCallBack(new MessageEventErrorReponse() { MsgCorelationId = args.MsgCorelationId, ErrorMessage = "Message Parsing Error" });
            }
            else {

                var callbackcast = (Action<IEventReponse>)callback;
                var errorcallbackcast = (Action<IEventErrorResponse>)errorCallBack;

                _eventBus.Trigger(TradeDomainObject, HandlerFactory.TradeAction, callbackcast, errorcallbackcast);
            }
        }
    }
}
