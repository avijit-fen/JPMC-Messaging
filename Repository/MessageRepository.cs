using Messaging.FakeDB;
using Messaging.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
    public class MessageRepository : IMessageRepositroy
    {
        
        private MessageDBContext _MsgDbctx;

        public MessageRepository() {

            _MsgDbctx = new MessageDBContext();
        }

        public void Add(Message message) {
            var msgs = _MsgDbctx.Messages;
            msgs.Add(message);
            _MsgDbctx.Messages = msgs;

        }

        public void Update(Message message)
        {
            var msgs = _MsgDbctx.Messages;
            var m = msgs.Find(p=>p.Id == message.Id);
            if (m != null) { m.IsProcessed = message.IsProcessed; m.Error = message.Error; m.IsError = message.IsError; }
            _MsgDbctx.Messages = msgs;
        }

        public List<Message> GetAll()
        {
            return _MsgDbctx.Messages.Where(p=>p.IsProcessed == true).ToList();
            
        }
    }
}
