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
    /// Trade Sales Handler to Register Trade
    /// </summary>
    public class TradeSalesHandler : IIntegrationHanlder<TradeEvent,TradeEventReponse,TradeEventResponseError>
    {
        private ITradeSalesService _tradeSalesService;
        public TradeSalesHandler(ITradeSalesService tradeSalesService) {

            _tradeSalesService = tradeSalesService;
        }

        public void Execute(TradeEvent args,Action<TradeEventReponse> callback, Action<TradeEventResponseError> e)
        {
            TradeSales tradeSales = null;
            try
            {

                if (args != null && !string.IsNullOrEmpty(args.TradeType))
                {
                    tradeSales = new TradeSales()
                    {
                        TradeType = args.TradeType,
                        Value = args.Value,
                        TradeQuantity = args.TradeQuantity,
                        UnitPrice = args.UnitPrice,
                        Operation = args.Operation,
                        InitialValue = args.Value
                    };

                }
                switch (args.ServiceOperation)
                {
                    case Constants.ADD:
                        _tradeSalesService.Add(tradeSales);
                        if (callback != null) callback(new TradeEventReponse() { MsgCorelationId = args.MsgCorelationId});
                        break;
                    case Constants.UPDATE:
                        _tradeSalesService.Update(tradeSales);
                        if (callback != null) callback(new TradeEventReponse() { MsgCorelationId = args.MsgCorelationId });
                        break;
                    case Constants.GET:
                        var sales = _tradeSalesService.GetAll();
                        if (callback != null) callback(new TradeEventReponse() { Result = sales });
                        break;

                }
            }
            catch(Exception ex)
            {
                if(e != null) { e(new TradeEventResponseError() { ErrorMessage = ex.Message, MsgCorelationId = args.MsgCorelationId }); }
            }
        }
    }

    

    
}
