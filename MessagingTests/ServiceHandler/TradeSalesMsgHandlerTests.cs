using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messaging.ServiceHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Messaging.POCO;

namespace Messaging.Tests
{
    [TestClass()]
    public class TradeSalesMsgHandlerTests
    {
        [TestMethod()]
        public void Execute_Test_Get()
        {
            var service = new Mock<ITradeSalesService>();
            List<TradeSales> trades = new List<TradeSales>() { It.IsAny<TradeSales>() };
            service.Setup(p => p.GetAll()).Returns(trades);
            var eventBus = new Mock<IEventBus>();
            eventBus.Setup(p => p.Trigger(null, null, null, null));
            var msgHandler = new TradeSalesHandler(service.Object);
            msgHandler.Execute(new TradeEvent() { ServiceOperation = "Get" }, null, null);
            Assert.IsTrue(true);
            
        }

        [TestMethod()]
        public void Execute_Test_Add()
        {
            var service = new Mock<ITradeSalesService>();
            service.Setup(p => p.Add(It.IsAny<TradeSales>())).Returns(It.IsAny<TradeSales>());
            var msgHandler = new TradeSalesHandler(service.Object);
            msgHandler.Execute(new TradeEvent() { ServiceOperation = "Add" }, null, null);
            Assert.IsTrue(true);
        }
    }
}