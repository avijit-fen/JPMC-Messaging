using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.POCO
{
    public interface IService<T>
    {
        T Add(T item);

        Task<T> AddAsync(T item);
        List<T> GetAll();

        void Update(T item);
    }

    public interface IMessageService : IService<Message> { }

    public interface ITradeSalesService : IService<TradeSales> { }
}
