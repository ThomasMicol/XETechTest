using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XETechTest.Core.Services.PointOfSaleFactory
{
    /// <summary>
    /// Responsible for building up a Point of sale service to be used by client code.
    /// </summary>
    public interface IPointOfSaleFactory
    {
        /// <summary>
        /// Returns Point of sale objects that conform to the IPointOfSaleService
        /// </summary>
        /// <returns>Not null object that conforms to the IPointOfSaleService interface</returns>
        IPointOfSaleService CreatePointOfSaleService();
    }
}
