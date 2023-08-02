using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.POCO
{
    /// <summary>
    /// Trade related Objects
    /// </summary>
    public class TradeSales
    {

        public Guid SalesId { get; set; }
        public string TradeType{ get; set; }
        public decimal Value { get; set; }
        public bool Adjusted { get; set; }
        public int TradeQuantity { get; set; }
        public decimal DeltaAdjustment { get; set; }
        public decimal UnitPrice { get; set; }
        public string Operation { get; set; }
        public decimal InitialValue { get; set; }
        public int Revision { get; set; }
        public List<TradeSalesHistory> TradeSalesHistory { get; set; }


    }

    public class TradeSalesHistory
    {
        public Guid SalesId { get; set; }
        public string TradeType { get; set;}
        public decimal Value { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal InitialValue { get; set; }

        public int Revision { get; set; }

    }
    
    public class TradeEvent:IEvent
    {
        public string Operation { get; set; }
        public string TradeType { get; set; }
        public int TradeQuantity { get; set; }
        public decimal Value { get; set; }
        public bool IsAdjustMent { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid MsgCorelationId { get; set; }
        public string ServiceOperation { get; set; }
    }

    public class TradeEventReponse:IEventReponse
    {
        public Guid MsgCorelationId { get; set; }
        public dynamic Result { get; set; }
        public string ServiceOperation { get; set; }
    }


    public class TradeEventResponseError : IEventErrorResponse
    {
        public Guid MsgCorelationId { get; set;}
        public string ErrorMessage { get; set;}
    }

 
}
