using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messaging.ServiceHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messaging.POCO;

namespace Messaging.Tests
{
    [TestClass()]
    public class SalesHelperTests
    {
        [TestMethod()]
        public void Parse_Test()
        {
            var helper = new SalesHelper();
            var msevent = new MessageEvent() { Message = "apple at 10p" , MsgCorelationId = Guid.NewGuid() };
            var tradeEvent = helper.Parse(msevent);
            Assert.IsNotNull(tradeEvent);
            Assert.IsNotNull(tradeEvent.Value);
            Assert.AreEqual(10, tradeEvent.UnitPrice);
            Assert.AreEqual(1, tradeEvent.TradeQuantity);
        }

        public void Parse_Test_1()
        {
            var helper = new SalesHelper();
            var msevent = new MessageEvent() { Message = "20 sales of apples at 10p", MsgCorelationId = Guid.NewGuid() };
            var tradeEvent = helper.Parse(msevent);
            Assert.IsNotNull(tradeEvent);
            Assert.IsNotNull(tradeEvent.Value);
            Assert.AreEqual(10, tradeEvent.UnitPrice);
            Assert.AreEqual(20, tradeEvent.TradeQuantity);
            Assert.AreEqual("apple", tradeEvent.TradeType);
        }

        public void Parse_Test_2()
        {
            var helper = new SalesHelper();
            var msevent = new MessageEvent() { Message = "Add 20p apples", MsgCorelationId = Guid.NewGuid() };
            var tradeEvent = helper.Parse(msevent);
            Assert.IsNotNull(tradeEvent);
            Assert.AreEqual(20, tradeEvent.Value);
            Assert.AreEqual("add", tradeEvent.Operation);
            Assert.AreEqual("apple", tradeEvent.TradeType);
        }

        [TestMethod()]
        public void Parse_Test_Failure()
        {
            var helper = new SalesHelper();
            var msevent = new MessageEvent() { Message = "at 10p", MsgCorelationId = Guid.NewGuid() };
            var tradeEvent = helper.Parse(msevent);
            Assert.IsNull(tradeEvent);
            
        }
    }
}