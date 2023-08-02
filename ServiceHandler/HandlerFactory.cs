using Messaging.Log;
using Messaging.POCO;
using Ninject;
using System;
using System.Reflection;

namespace Messaging.ServiceHandler
{
    /// <summary>
    /// Repository of action handler factory
    /// </summary>
    public static class HandlerFactory
    {
        static StandardKernel Kernel { get; set; }
        static HandlerFactory()
        {
            Kernel = new StandardKernel();
            Kernel.Load(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Handlter for Trade Action
        /// </summary>
        public static Action<string, IEvent , Action<IEventReponse> , Action<IEventErrorResponse>> TradeAction = (e, v ,w, y) =>
        {
            new TradeSalesHandler(Kernel.Get<ITradeSalesService>()).Execute((TradeEvent)v,w,y);
        };

        
       public static Action<IEventErrorResponse> erroAction = (u) =>
       {

       };

        public static Action<string, IEvent, Action<IEventReponse>, Action<IEventErrorResponse>> TradeMessageAction = (e, v, w, y) =>
        {
            new TradeSaleMsgHandler(EventBus.Instance).Execute((MessageEvent)v, w, y);
        };

        public static Action<string, IEvent, Action<IEventReponse>, Action<IEventErrorResponse>> MessageAction = (e, v, w, y) =>
        {
            new MessageHandler(Kernel.Get<IMessageService>(), EventBus.Instance).Execute((MessageEvent)v, w, y);
        };

        public static Action<string, IEvent, Action<IEventReponse>, Action<IEventErrorResponse>> ReportAction = (e, v, w, y) =>
        {
            new TradeReportHandler(Kernel.Get<ILogger>(),EventBus.Instance).Execute((ReportEvent)v, w, y);
        };


    }
}
