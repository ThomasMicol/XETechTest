using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XETechTest.Core.Models;
using XETechTest.Core.Services;
using XETechTest.Core.Services.PointOfSaleFactory;

namespace XETechTest.Tests
{
    [TestClass]
    public class PointOfSaleServiceTests
    {
        private IPointOfSaleFactory _pointOfSaleFactory;

        [TestInitialize]
        public void TestSetup()
        {
            _pointOfSaleFactory = new PointOfSaleFactory();
        }

        [TestMethod] 
        public void ConstructNewPointOfSaleObjectIsNotNull() 
        {
            var service = new PointOfSaleService(new List<IProduct>());

            Assert.IsNotNull(service);
        }

        [DataRow("ABCDABA", 13.25)]
        [DataRow("CCCCCCC", 6.00)]
        [DataRow("ABCD", 7.25)] 
        [DataRow("ACCABCACCCCCCACACCCCADA", 24.25)]
        [DataRow("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", 62.50)]
        [DataRow("", 0)]
        [DataRow("     ", 0)]
        [DataRow("    BBB ", 12.75)]
        [DataRow("  AA  A A  ", 4.25)]
        [DataRow(" a BB dbdb b AA   cccCcfhebcCcc ", 38)]
        [DataRow("qwres oisjfd wo", 0.75)]
        [DataTestMethod]
        public void FactoryPointOfSaleObjectScansItemsAndReturnsCorrectTotals(string productCodeSet, double expectedValue)
        {
            var PoSService = _pointOfSaleFactory.CreatePointOfSaleService();

            // split up the product code string that was passed into this method. This relies heavily on the assumption that all codes are only ever 
            // going to be one character long 
            var productCodeList = productCodeSet.ToCharArray().Select(a => a.ToString()).ToList();

            Assert.IsNotNull(productCodeList);

            // loop through the list of product codes scanning them into the basket
            foreach (var productCode in productCodeList)
            {
                PoSService.ScanProduct(productCode);
            }

            // once we are done scanning get the total basket amount and assert that it is inline with the expected value
            var totalResult = PoSService.CalculateTotal();

            Assert.AreEqual((decimal)expectedValue, totalResult);
        }
    }
}
