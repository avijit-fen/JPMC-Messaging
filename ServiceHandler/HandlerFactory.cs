using Messaging.Log;
using Messaging.POCO;
using Messaging.ServiceHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.ServiceHandler
{
    /// <summary>
    /// Repository of action handler factory
    /// </summary>
    public static class HandlerFactory
    {
        public static Action<string, IEvent , Action<IEventReponse> , Action<IEventErrorResponse>> TradeAction = (e, v ,w, y) =>
        {
            new TradeSalesHandler(new Service.TradeSalesService(new Repository.TradeSalesRepository())).Execute((TradeEvent)v,w,y);
        };

        public static string Add = "Add";
        public static string Update = "Update";

       public static Action<IEventErrorResponse> erroAction = (u) =>
       {

       };

        public static Action<string, IEvent, Action<IEventReponse>, Action<IEventErrorResponse>> TradeMessageAction = (e, v, w, y) =>
        {
            new TradeSaleMsgHandler(EventBus.Instance).Execute((MessageEvent)v, w, y);
        };

        public static Action<string, IEvent, Action<IEventReponse>, Action<IEventErrorResponse>> MessageAction = (e, v, w, y) =>
        {
            new MessageHandler(new Service.MessageService(new MessageRepository()),EventBus.Instance).Execute((MessageEvent)v, w, y);
        };

        public static Action<string, IEvent, Action<IEventReponse>, Action<IEventErrorResponse>> ReportAction = (e, v, w, y) =>
        {
            new TradeReportHandler(new Logger(),EventBus.Instance).Execute((ReportEvent)v, w, y);
        };


    }
}
