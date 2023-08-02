using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.POCO
{
    /// <summary>
    /// TradeReport related objects
    /// </summary>
    public class TradeReport
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public string TradeType { get; set; }
        public decimal Sales { get; set; }
    }

    public class TradeAdjustmentReport
    {
        public Guid Id { get; set; }
        public string TradeType { get; set; }
        public decimal SalesAdjustMents { get; set; }
    }


    public class ReportEventResponse : IEventReponse
    {
        public Guid MsgCorelationId { get; set; }
        public string ServiceOperation { get; set; }

    }

    public class ReportEventReponseError : IEventErrorResponse
    {
        public Guid MsgCorelationId { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ReportEvent : IEvent
    {
        public Guid MsgCorelationId { get; set; }
        public string ServiceOperation { get; set; }

    }
}
