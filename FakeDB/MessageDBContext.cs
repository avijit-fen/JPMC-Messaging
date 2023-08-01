using Messaging.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.FakeDB
{
    public class MessageDBContext
    {
        ObjectCache cache;
        public MessageDBContext()
        {
            cache = MemoryCache.Default;
        }

        public List<Message> Messages
        {

            get
            {
                if (cache["Messages"] == null) return new List<Message>();
                else { return (List<Message>)cache["Messages"]; }

            }

            set { cache["Messages"] = value; }

        }
    }
}
