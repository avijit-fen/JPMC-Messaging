using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.POCO
{
    public interface IRepository<T>
    {
        void Add(T item);
        List<T> GetAll();

        void Update(T item);
    }

    public interface IMessageRepositroy : IRepository<Message>
    {

    }

    public interface ITradeSalesRepository : IRepository<TradeSales> { }
}
