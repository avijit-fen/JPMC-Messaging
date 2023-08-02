using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Repository
{
    public static class ServiceHelper
    {
        public static double CalculateSales(string operation, double unitPrice , double salesQuantity , double adjustment)
        {
            double salesVal = 0;
            switch (operation)
            {
                case "add":
                    salesVal = (unitPrice + adjustment)  * salesQuantity;
                    break;
                case "multiply":
                    salesVal = (unitPrice * adjustment) * salesQuantity;
                    break;
                case "substract":
                    salesVal = (unitPrice - adjustment) * salesQuantity;
                    break;
                default:
                    break;
            }

            return salesVal;
        }

        public static double CalculateUnitPrice(string operation, double unitPrice, double adjustment)
        {
            double salesVal = 0;
            switch (operation)
            {
                case "add":
                    salesVal = unitPrice + adjustment;
                    break;
                case "multiply":
                    salesVal = unitPrice * adjustment;
                    break;
                case "substract":
                    salesVal = unitPrice >= adjustment ? (unitPrice - adjustment) : 0;
                    break;
                default:
                    break;
            }

            return salesVal;
        }


    }
}
