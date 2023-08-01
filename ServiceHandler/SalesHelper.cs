using Messaging.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Messaging.ServiceHandler
{
    /// <summary>
    /// Sales Helper to parse message
    /// </summary>
    public class SalesHelper
    {
        private Dictionary<string, Regex> regexes;

        public SalesHelper() {

            regexes = new Dictionary<string, Regex>();
            regexes.Add("SINGLESALE", new Regex("^(\\w+)\\s+at\\s+(\\d+)[pP]$"));
            regexes.Add("MULTIPLESALE", new Regex("^(\\d+) sales of (\\w+) at (\\d+)p each$"));
            regexes.Add("ADJUSTMENT", new Regex("(\\w+) (\\d+)p (\\w+)"));

        }
        /// <summary>
        /// Converting message vent to sales event
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public TradeEvent Parse(MessageEvent msg)
        {
            foreach (var pair in regexes)
            {
                var match = pair.Value.Match(msg.Message);
                if (match.Success)
                {
                    return Calculate(pair.Key, match.Groups,msg.MsgCorelationId);
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string ToLower(string s)
        {
            return s.ToLower();
        }
        /// <summary>
        /// Format tradetype 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string FormatTradeType(string type)
        {
            if(string.IsNullOrEmpty(type)) { return string.Empty; }

            if (type.EndsWith("s",StringComparison.InvariantCultureIgnoreCase))
            {
                return ToLower(type.Substring(0, type.Length - 1).ToLower());
            }

            return type;
        }
        /// <summary>
        /// actual method to create tradevent
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groups"></param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        private TradeEvent Calculate(string key , GroupCollection groups, Guid msgId)
        {
            TradeEvent sale = null;

            if(key == "SINGLESALE")
            {
                sale = new TradeEvent()
                {
                    Operation = "addsingle",
                    TradeType = FormatTradeType(groups[1].Value),
                    Value = int.Parse(groups[2].Value),
                    UnitPrice = int.Parse(groups[2].Value),
                    TradeQuantity = 1,
                    MsgCorelationId = msgId,
                    ServiceOperation = "Add"
                };
            }
            if(key == "MULTIPLESALE")
            {
                sale = new TradeEvent()
                {
                    Operation = "addmultiple",
                    TradeType = FormatTradeType(groups[2].Value),
                    Value = int.Parse(groups[3].Value) * int.Parse(groups[1].Value),
                    UnitPrice = int.Parse(groups[3].Value),
                    TradeQuantity = int.Parse(groups[1].Value),
                    MsgCorelationId = msgId,
                    ServiceOperation = "Add"
                };
            }
            if (key == "ADJUSTMENT")
            {
                sale = new TradeEvent()
                {
                    Operation = ToLower(groups[1].Value),
                    TradeType = FormatTradeType(groups[3].Value),
                    Value = int.Parse(groups[2].Value),
                    IsAdjustMent = true,
                    MsgCorelationId = msgId,
                    ServiceOperation = "Update"
                };
            }

            return sale;

        }

    }
}
