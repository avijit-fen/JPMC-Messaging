using Messaging.FakeDB;
using Messaging.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Repository
{
    public class TradeSalesRepository : ITradeSalesRepository
    {
        private SalesDbContext _tradesalesDbctx;
        private List<TradeSales> TradeSales { get; set; }

        public TradeSalesRepository() {

            _tradesalesDbctx = new SalesDbContext();
        }

        public void Add(TradeSales tradeSales)
        {
            tradeSales.SalesId = Guid.NewGuid();
            var sales = _tradesalesDbctx.TradeSales;
            sales.Add(tradeSales);
            _tradesalesDbctx.TradeSales = sales;
        }

        public void Update(TradeSales tradesales)
        {
            var trades = _tradesalesDbctx.TradeSales;
            var sales = trades.Find(p => p.SalesId == tradesales.SalesId);
            if(sales != null)
            {
                sales.Value = tradesales.Value;
            }
            _tradesalesDbctx.TradeSales = trades;

        }

        public List<TradeSales> GetAll()
        {
            return _tradesalesDbctx.TradeSales;
        }

        public void Update(List<TradeSales> tradeSales)
        {
            _tradesalesDbctx.TradeSales = tradeSales;
        }
    }
}
