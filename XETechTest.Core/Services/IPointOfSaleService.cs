using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XETechTest.Core.Services
{
    /// <summary>
    /// Interface defining the way client code can interact with PointOfSaleServices. 
    /// </summary>
    public interface IPointOfSaleService
    {
        /// <summary>
        /// Calculates the total of the current basket of items.
        /// </summary>
        /// <returns>A decimal representing the sum total of all products currently scanned.</returns>
        decimal CalculateTotal();

        /// <summary>
        /// Adds a product to the current running list of products
        /// </summary>
        /// <param name="productCode">Code of the prodcuct being added to the current basket of items.</param>
        void ScanProduct(string productCode);
    }
}
