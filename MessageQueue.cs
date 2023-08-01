using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
    internal class MessageQueue : Queue<string>
    {
        public event EventHandler<QueueNotificationEvent> OnMessage;

        public MessageQueue() :base() { }

        public MessageQueue(int  capacity) : base(capacity) { }
        /// <summary>
        /// Enqueue msg method
        /// </summary>
        /// <param name="msg"></param>
        public void EnqueueMsg(string msg)
        {
            base.Enqueue(msg);
        }
        /// <summary>
        /// Dequeue message and Invoke notification
        /// </summary>
        public void DequeueMsg()
        {
            var msg = base.Dequeue();
            OnMessage?.Invoke(this, new QueueNotificationEvent() { Message = msg });

        }

    }
}
