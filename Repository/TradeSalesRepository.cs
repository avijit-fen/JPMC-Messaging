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
            var totaltradesales = _tradesalesDbctx.TradeSales;
            var sales = totaltradesales.Where(p => p.TradeType == tradesales.TradeType);
            
            foreach (var trade in sales)
            {
                var adjustedVal = RepositoryHelper.CalculateSales(tradesales.Operation, trade.UnitPrice, trade.TradeQuantity, tradesales.Value);
                trade.DeltaAdjustment = adjustedVal - trade.InitialValue;
                trade.Value = adjustedVal;
                trade.Adjusted = true;
                trade.UnitPrice = RepositoryHelper.CalculateUnitPrice(tradesales.Operation,trade.UnitPrice, tradesales.Value);
            }
            _tradesalesDbctx.TradeSales = totaltradesales;

        }

        public List<TradeSales> GetAll()
        {
            return _tradesalesDbctx.TradeSales;
        }
    }
}
