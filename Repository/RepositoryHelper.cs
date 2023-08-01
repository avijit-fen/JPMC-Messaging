using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Repository
{
    internal static class RepositoryHelper
    {
        public static int CalculateSales(string operation, int unitPrice , int salesQuantity , int adjustment)
        {
            int salesVal = 0;
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

        public static int CalculateUnitPrice(string operation, int unitPrice, int adjustment)
        {
            int salesVal = 0;
            switch (operation)
            {
                case "add":
                    salesVal = unitPrice + adjustment;
                    break;
                case "multiply":
                    salesVal = unitPrice * adjustment;
                    break;
                case "substract":
                    salesVal = unitPrice - adjustment;
                    break;
                default:
                    break;
            }

            return salesVal;
        }


    }
}
