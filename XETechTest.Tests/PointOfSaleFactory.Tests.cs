using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XETechTest.Core.Services.PointOfSaleFactory;

namespace XETechTest.Tests
{
    [TestClass]
    public class PointOfSaleFactoryTests
    {
        [TestMethod]
        public void FactoryCreatePointOfSaleReturnsCorrectPOSObject()
        {
            var factory = new PointOfSaleFactory();

            var PoSService  = factory.CreatePointOfSaleService();

            Assert.IsNotNull(PoSService);
        }
    }
}
