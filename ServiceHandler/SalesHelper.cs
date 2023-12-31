﻿using Messaging.POCO;
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
            regexes.Add("SINGLESALE", new Regex(Config.GetValue("SINGLESALE")));
            regexes.Add("MULTIPLESALE", new Regex(Config.GetValue("MULTIPLESALE")));
            regexes.Add("ADJUSTMENT", new Regex(Config.GetValue("ADJUSTMENT")));

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
                if(string.IsNullOrEmpty(msg.Message))
                {
                    return null;
                }
                var match = pair.Value.Match(ToLower(msg.Message));
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
            return string.IsNullOrEmpty(s) ? string.Empty : s.ToLower();
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
        /// Parse to double
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private decimal Parse(string s)
        {
            if (string.IsNullOrEmpty(s)) { return 0; }
            decimal res = 0;
            if (decimal.TryParse(s, out res)) { return res; }

            return res;
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
                    Value = Parse(groups[2].Value),
                    UnitPrice = Parse(groups[2].Value),
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
                    Value = Parse(groups[3].Value) * Parse(groups[1].Value),
                    UnitPrice = Parse(groups[3].Value),
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
                    TradeType = FormatTradeType(groups[4].Value),
                    Value = Parse(groups[2].Value),
                    IsAdjustMent = true,
                    MsgCorelationId = msgId,
                    ServiceOperation = "Update"
                };
            }

            return sale;

        }

    }
}
