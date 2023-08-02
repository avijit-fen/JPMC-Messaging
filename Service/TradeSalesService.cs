using Messaging.POCO;
using Messaging.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Service
{
    public class TradeSalesService : ITradeSalesService
    {
        private ITradeSalesRepository _repository;
        
         
        public TradeSalesService(ITradeSalesRepository tradeSalesRepository) {

            _repository = tradeSalesRepository;
            
        }
        public TradeSales Add(TradeSales msg)
        {
            _repository.Add(msg);
            return msg;
        }

        public Task<TradeSales> AddAsync(TradeSales item)
        {
            throw new NotImplementedException();
        }

        public List<TradeSales> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(TradeSales tradesales)
        {
            var tradeSales = _repository.GetAll();
            var sales = tradeSales.Where(p => p.TradeType == tradesales.TradeType);

            foreach (var trade in sales)
            {
                double adjustedVal = ServiceHelper.CalculateSales(tradesales.Operation, trade.UnitPrice, trade.TradeQuantity, tradesales.Value);
                trade.DeltaAdjustment = adjustedVal - trade.InitialValue;
                trade.Value = adjustedVal;
                trade.Adjusted = true;
                trade.UnitPrice = ServiceHelper.CalculateUnitPrice(tradesales.Operation, trade.UnitPrice, tradesales.Value);
            }

            _repository.Update(tradesales);
        }
    }
}
