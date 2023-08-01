using Messaging.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.FakeDB
{
    public class SalesDbContext
    {
        ObjectCache cache;
        public SalesDbContext() {

            cache = MemoryCache.Default;
        }
        
        public List<TradeSales> TradeSales
        { 
            
          get { if (cache["TradeSales"] == null) return new List<TradeSales>();
                else { return (List<TradeSales>)cache["TradeSales"]; }
                
            }
            
          set { cache["TradeSales"] = value; }
        
        }

        

    }
}
