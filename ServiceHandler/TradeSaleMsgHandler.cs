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
        ILogger _logger { get; set; }
        public TradeSaleMsgHandler(IEventBus eventBus, ILogger logger)
        {

            _eventBus = eventBus;
            _logger = logger;
        }
        public void Execute(MessageEvent args, Action<MessageEventResponse> callback, Action<MessageEventErrorReponse> errorCallBack)
        {
            var helper = new SalesHelper();
            var TradeDomainObject = helper.Parse(args);
            if (TradeDomainObject == null)
            {
                _logger.Warn("Message not in correct format:" + args.Message);
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
