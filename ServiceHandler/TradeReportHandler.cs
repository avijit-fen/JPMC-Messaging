using Messaging.Log;
using Messaging.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.ServiceHandler
{
    /// <summary>
    /// Trade report handler
    /// </summary>
    public class TradeReportHandler : IIntegrationHanlder<ReportEvent,ReportEventResponse,ReportEventReponseError>
    {
        IEventBus _eventBus { get; set; }
        ILogger _logger;
        public TradeReportHandler(ILogger logger , IEventBus eventBus)
        {
            _eventBus = eventBus;
            _logger = logger;
        }
        /// <summary>
        /// execute 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="callback"></param>
        /// <param name="errorCallBack"></param>
        public void Execute(ReportEvent args, Action<ReportEventResponse> callback, Action<ReportEventReponseError> errorCallBack)
        {
            List<TradeReport> result = new List<TradeReport>();

            if(args != null && args.ServiceOperation == Constants.TRANSACTIONREPORT)
            {
                Action<IEventReponse> action = (obj) =>
                {
                    var sales = (List<TradeSales>)(obj as TradeEventReponse).Result;

                    result = sales
                        .GroupBy(l => l.TradeType)
                        .Select(cl => new TradeReport
                        {
                            Count = cl.Sum(c => c.TradeQuantity),
                            TradeType = cl.First().TradeType,
                            Sales = cl.Sum(c => c.Value),
                        }).ToList();

                    _logger.Info("Trade Reports");
                    _logger.Info("TradeType" + " " + "TotalSales" + " " + "SalesCount");

                    result.ForEach(t =>
                    {
                        _logger.Info(t.TradeType + " " + $"{t.Sales}p" + " " + t.Count);

                    });
                };

                var domain = new TradeEvent() { ServiceOperation = Constants.GET };
                _eventBus.Trigger(domain, HandlerFactory.TradeAction, action, HandlerFactory.erroAction);

            }
            if(args != null && args.ServiceOperation == Constants.TRANSACTIONADJUSTREPORT)
            {
                Action<IEventReponse> action = (obj) =>
                {
                    var sales = (List<TradeSales>)(obj as TradeEventReponse).Result;

                    List<TradeAdjustmentReport> tardeAdjustmentresult = sales.Where(p => p.Adjusted)
                            .Select(cl => new TradeAdjustmentReport()
                            {
                                Id = cl.SalesId,
                                TradeType = cl.TradeType,
                                SalesAdjustMents = cl.DeltaAdjustment
                            }).ToList();

                    if (tardeAdjustmentresult.Count == 0)
                    {
                        _logger.Info("No adjustment");
                    }
                    else
                    {
                        _logger.Info("Total Adjustments:" +  tardeAdjustmentresult.Count);
                        _logger.Info("SalesId" + " " + "TradeType" + " " + "SalesAdjustment");
                        tardeAdjustmentresult.ForEach(t =>
                        {
                            _logger.Info(t.Id + " " + t.TradeType + " " + $"{t.SalesAdjustMents}p");
                        });
                    }

                };

                var domain = new TradeEvent() { ServiceOperation = Constants.GET};
                _eventBus.Trigger(domain, HandlerFactory.TradeAction, action, HandlerFactory.erroAction);
            }


        }
    }
}
