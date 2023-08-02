using Messaging.Log;
using Messaging.POCO;
using Messaging.ServiceHandler;
using Messaging.TestData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Messaging
{
    public class Program
    {
        static ILogger logger = new Logger();
        static void Main(string[] args)
        {
            
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);

            try
            {
                var list = TestDataGen.generate();
                Initialize(list);
                
            }
            catch(Exception ex)
            {
                logger.Error("Error Occured",ex);
            }
            
        }

        private static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args) 
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("ExceptionHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
            Console.ReadLine();
        }

        private static void Initialize(List<string> msgs)
        {
            MessageQueue queue = new MessageQueue(200000);
            EventBus eventBus = EventBus.Instance;
            MessageBroker broker = new MessageBroker(eventBus);
            broker.Subscribe(eventBus);
            broker.Subscribe(queue);
            broker.OnMessage += Broker_OnMessage;

            Parallel.ForEach(msgs, new ParallelOptions { MaxDegreeOfParallelism = 4 }, p =>
            {
                queue.EnqueueMsg(p);
            });

            while(queue.Count != 0)
            {
                queue.DequeueMsg();
            }
        }

        /// <summary>
        /// This will execute once message come out of queue / dequeue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Broker_OnMessage(object sender, NotificationEventArgBroker e)
        {
             EventBus eventBus = EventBus.Instance;
             

            int reportingInterval = int.Parse(Config.GetValue("reportingInterval"));
            int pausingInterval = int.Parse(Config.GetValue("PauseInterval"));

            if (e.Count > 0 && e.Count % reportingInterval == 0)
            {
                var r = new ReportEvent() { ServiceOperation = Constants.TRANSACTIONREPORT };
                eventBus.Trigger(r, HandlerFactory.ReportAction, null, null);
            }
            if (e.Count == pausingInterval)
            {
                logger.Info("Pausing");
                var r = new ReportEvent() { ServiceOperation = Constants.TRANSACTIONADJUSTREPORT };
                eventBus.Trigger(r, HandlerFactory.ReportAction, null, null);
                Thread.Sleep(-1);
                
            }
        }

        
    }

    
}
