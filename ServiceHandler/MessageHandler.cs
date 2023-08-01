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
    /// Message handler - would be called when msg arrives
    /// </summary>
    public class MessageHandler : IIntegrationHanlder<MessageEvent, MessageEventResponse, MessageEventErrorReponse>
    {
        private IMessageService _service { get; set; }
        IEventBus _eventBus { get; set; }
        public MessageHandler(IMessageService messageService,IEventBus eventBus) {

            _eventBus = eventBus;
            _service = messageService;
        }
        /// <summary>
        /// Execute method
        /// </summary>
        /// <param name="args"></param>
        /// <param name="callback"></param>
        /// <param name="errorCallBack"></param>
        public void Execute(MessageEvent args, Action<MessageEventResponse> callback, Action<MessageEventErrorReponse> errorCallBack)
        {

            switch(args.ServiceOperation)
            {
                case Constants.ADD:
                    var v = _service.Add(new Message() { Id = args.MsgCorelationId, Text = args.Message });
                    if(v.Id != null)
                    {
                        var msgDomain = new MessageEvent()
                        {
                            MsgCorelationId = args.MsgCorelationId,
                            Message = args.Message
                        };

                        Action<IEventReponse> callbackHandler = (o) =>
                        {
                            Guid MsgId = (o as TradeEventReponse).MsgCorelationId;
                            _service.Update(new Message { Id = MsgId, IsProcessed = true });
                            if (callback != null) { callback(new MessageEventResponse() { MsgCorelationId = MsgId }); }
                        };

                        Action<IEventErrorResponse> errorcallbackHandler = (o) =>
                        {
                            Guid MsgId = o.MsgCorelationId;
                            _service.Update(new Message { Id = MsgId, IsProcessed = true, Error = o.ErrorMessage, IsError = true });
                            if (callback != null) { callback(new MessageEventResponse() { MsgCorelationId = MsgId }); }
                        };

                        _eventBus.Trigger(msgDomain, HandlerFactory.TradeMessageAction, callbackHandler, errorcallbackHandler);
                    }
                    

                    break;

                case Constants.GET:
                    var count = _service.GetAll().Count;
                    if (callback != null) { callback(new MessageEventResponse() { Result = count }); }
                    break;

            }

            
        }
    }
}
