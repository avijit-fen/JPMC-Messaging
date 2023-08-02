using Messaging.Log;
using Messaging.POCO;
using Messaging.Repository;
using Messaging.Service;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging
{
    public class NinjectModuleBinder : NinjectModule
    {
        public override void Load()
        {
            Bind<IMessageRepositroy>().To<MessageRepository>();
            Bind<ITradeSalesRepository>().To<TradeSalesRepository>();
            Bind<ITradeSalesService>().To<TradeSalesService>();
            Bind<IMessageService>().To<MessageService>();
            Bind<ILogger>().To<Logger>();

        }
    }
}
