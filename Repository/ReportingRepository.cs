using Messaging.FakeDB;
using Messaging.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Repository
{
    public class ReportingRepository
    {
        ReportingDBContext reportingDBCtx;
        public ReportingRepository() {

            reportingDBCtx = new ReportingDBContext();
        }

        public void Add(List<TradeSales> tradeSales)
        {
            reportingDBCtx.TradeSales = tradeSales;
            
        }
        public List<TradeSales> GetAll()
        {
            return reportingDBCtx.TradeSales;
        }
    }
}
