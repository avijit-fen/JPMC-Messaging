using Microsoft.VisualStudio.TestTools.UnitTesting;
using Messaging.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Tests
{
    [TestClass()]
    public class ServiceHelperTests
    {
        [TestMethod()]
        public void CalculateSales_Test()
        {
            string operation = "add";
            int tradequantity = 1;
            decimal unitprice = 78;
            decimal adjustment = 100;

            decimal val = ServiceHelper.CalculateSales(operation,unitprice,tradequantity,adjustment);
            decimal price = ServiceHelper.CalculateUnitPrice(operation, unitprice, adjustment);
            Assert.AreEqual(178,val);
            Assert.AreEqual(178, price);
        }

        [TestMethod()]
        public void CalculateSales_Test_Multiple()
        {
            string operation = "add";
            int tradequantity = 3;
            decimal unitprice = 50;
            decimal adjustment = 50;

            decimal val = ServiceHelper.CalculateSales(operation, unitprice, tradequantity, adjustment);
            decimal price = ServiceHelper.CalculateUnitPrice(operation, unitprice, adjustment);
            Assert.AreEqual(300, val);
            Assert.AreEqual(100, price);
        }

        [TestMethod()]
        public void CalculateSales_Test_Multiply()
        {
            string operation = "multiply";
            int tradequantity = 3;
            decimal unitprice = 5;
            decimal adjustment = 6;

            decimal val = ServiceHelper.CalculateSales(operation, unitprice, tradequantity, adjustment);
            decimal price = ServiceHelper.CalculateUnitPrice(operation, unitprice, adjustment);
            Assert.AreEqual(90, val);
            Assert.AreEqual(30, price);
        }

        [TestMethod()]
        public void CalculateSales_Test_Add_Decimal()
        {
            string operation = "add";
            int tradequantity = 1;
            decimal unitprice = 78.32M;
            decimal adjustment = 100.20M;

            decimal val = ServiceHelper.CalculateSales(operation, unitprice, tradequantity, adjustment);
            decimal price = ServiceHelper.CalculateUnitPrice(operation, unitprice, adjustment);
            Assert.AreEqual(178.52M, val);
            Assert.AreEqual(178.52M, price);
        }

        [TestMethod()]
        public void CalculateSales_Test_multiply_Decimal()
        {
            string operation = "multiply";
            int tradequantity = 5;
            decimal unitprice = 2.50M;
            decimal adjustment = 7.30M;

            decimal val = ServiceHelper.CalculateSales(operation, unitprice, tradequantity, adjustment);
            decimal price = ServiceHelper.CalculateUnitPrice(operation, unitprice, adjustment);
            Assert.AreEqual(91.2500M, val);
            Assert.AreEqual(18.2500M, price);
        }

        [TestMethod()]
        public void CalculateSales_Test_substract_Decimal()
        {
            string operation = "multiply";
            int tradequantity = 5;
            decimal unitprice = 2.50M;
            decimal adjustment = .50M;

            decimal val = ServiceHelper.CalculateSales(operation, unitprice, tradequantity, adjustment);
            decimal price = ServiceHelper.CalculateUnitPrice(operation, unitprice, adjustment);
            Assert.AreEqual(6.2500M, val);
            Assert.AreEqual(1.2500M, price);
        }
        [TestMethod()]
        public void CalculateSales_Test_substract_Decimal_digit()
        {
            string operation = "multiply";
            int tradequantity = 5;
            decimal unitprice = 2.507888M;
            decimal adjustment = .509999M;

            decimal val = ServiceHelper.CalculateSales(operation, unitprice, tradequantity, adjustment);
            decimal price = ServiceHelper.CalculateUnitPrice(operation, unitprice, adjustment);
            Assert.AreEqual(6.39510M, val);
            Assert.AreEqual(1.27902M, price);
        }


        
    }
}