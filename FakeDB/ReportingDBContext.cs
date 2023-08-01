using Messaging.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.FakeDB
{
    public class ReportingDBContext
    {
        ObjectCache cache;
        public ReportingDBContext() 
        {
            cache = MemoryCache.Default;
        }
        private List<TradeSales> _tradesales;
        public List<TradeSales> TradeSales
        {

            get
            {
                if (cache["ReportSales"] == null) return new List<TradeSales>();
                else { return (List<TradeSales>)cache["ReportSales"]; }

            }

            set { cache["ReportSales"] = value; }

        }
    }
}
