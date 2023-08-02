using Messaging.ServiceHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Repository
{
    public static class ServiceHelper
    {

        static int _decimalPrecision;

        static ServiceHelper()
        {
            _decimalPrecision = int.Parse(Config.GetValue("SalesDecimal"));
        }

        /// <summary>
        /// calculates sales limiting to 5 decimal places
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="unitPrice"></param>
        /// <param name="salesQuantity"></param>
        /// <param name="adjustment"></param>
        /// <returns></returns>
        public static decimal CalculateSales(string operation, decimal unitPrice , int salesQuantity , decimal adjustment)
        {
            decimal salesVal = 0;
            switch (operation)
            {
                case "add":
                    salesVal = (unitPrice + adjustment)  * salesQuantity;
                    break;
                case "multiply":
                    salesVal = (unitPrice * adjustment) * salesQuantity;
                    break;
                case "substract":
                    salesVal = unitPrice > adjustment ? ((unitPrice - adjustment) * salesQuantity) : 0;
                    break;
                default:
                    break;
            }

            return salesVal > 0 ? Math.Round(salesVal, _decimalPrecision) : salesVal;
        }
        /// <summary>
        /// calculate new unit price limiting to 5 decmal places
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="unitPrice"></param>
        /// <param name="adjustment"></param>
        /// <returns></returns>
        public static decimal CalculateUnitPrice(string operation, decimal unitPrice, decimal adjustment)
        {
            decimal salesVal = 0;
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

            return salesVal > 0 ? Math.Round(salesVal, _decimalPrecision) : salesVal;
        }


    }
}
