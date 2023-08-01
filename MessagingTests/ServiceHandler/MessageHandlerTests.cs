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
    public class MessageHandlerTests
    {
        [TestMethod()]
        public void Execute_Test_Get()
        {
            var service = new Mock<IMessageService>();
            var msg = new Message() { Id = Guid.NewGuid() };
            List<Message> msgs = new List<Message>() {  msg };
            service.Setup(p=>p.Add(msg)).Returns(msg);
            service.Setup(p => p.GetAll()).Returns(msgs);
            var eventBus = new Mock<IEventBus>();
            eventBus.Setup(p => p.Trigger(null, null, null, null));
            var msgHandler = new MessageHandler(service.Object,eventBus.Object);
            msgHandler.Execute(new MessageEvent() { ServiceOperation = "Get" }, null, null);
            
        }

        [TestMethod()]
        public void Execute_Test_Add()
        {
            var service = new Mock<IMessageService>();
            var msg = new Message() { Id = Guid.NewGuid() , Text ="" };
            List<Message> msgs = new List<Message>() { msg };
            service.Setup(p => p.Add(It.IsAny<Message>())).Returns(msg);
            //service.Setup(p => p.GetAll()).Returns(msgs);
            var eventBus = new Mock<IEventBus>();
            eventBus.Setup(p => p.Trigger(null, null, null, null));
            var msgHandler = new MessageHandler(service.Object, eventBus.Object);
            msgHandler.Execute(new MessageEvent() { ServiceOperation = "Add" }, null, null);
        }
    }
}