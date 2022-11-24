using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XETechTest.Core.Models;

namespace XETechTest.Core.Services
{
    public class PointOfSaleService : IPointOfSaleService
    {
        /// <summary>
        /// A List of all products in the system. Used to look up prices when calculated
        /// </summary>
        private readonly IList<IProduct> _products;

        /// <summary>
        /// The current dictionary of items that have been scanned into the system
        /// </summary>
        private Dictionary<string, int> _shoppingBasket = new Dictionary<string, int>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="products">List of all products in the system, storing all products in memory could be an issue 
        /// if the number of products were to grow too large. Replacing this with a lookup service for products to get details
        /// from a database or other datastore could avoid keeping too much product information in memory</param>
        public PointOfSaleService(IList<IProduct> products)
        {
            _products = products;
        }

        /// <summary>
        /// goes through all the products in the basket and calculates the total cost for them
        /// </summary>
        /// <returns>decimal representing the total cost of all items</returns>
        public decimal CalculateTotal()
        {
            // initialize a counter for the running total
            decimal total = 0;

            foreach(var product in _shoppingBasket)
            {
                // get the pricing information for the current product 
                var productPricing = GetProductPricing(product.Key);

                // if no pricing for the product was found we move on to the next product in the basket
                if (productPricing == null)
                    continue;

                // add the result of the get price method to the running total counter
                total += productPricing.GetPrice(product.Value);
            }

            return total;
        }

        /// <summary>
        /// Scans a product into the basket
        /// </summary>
        /// <param name="productCode">the product code that will be used in cost calculation to look up pricing information</param>
        public void ScanProduct(string productCode)
        {
            // convert the scanned product code to a uniform format by removing leading a trailing spaces and forcing uppercase
            productCode = productCode.Trim().ToUpperInvariant();

            // if we are left with an empty string, return.
            if (productCode == String.Empty)
                return;

            // Attempt to get the product pricing, if none exists, return
            var productPricing = GetProductPricing(productCode);
            if (productPricing == null)
                return;

            // check if we already have a product with this code in the basket
            if (_shoppingBasket.Any(a => a.Key == productCode))
            {
                // if we do increment this products quantity by one
                _shoppingBasket[productCode] = _shoppingBasket[productCode] + 1;
            }
            else
            {
                // if we dont we add a new product to the dictionary with a starting quantity of one
                _shoppingBasket.Add(productCode, 1);
            }
        }

        /// <summary>
        /// Attempts to get product pricing information for a given product code
        /// </summary>
        /// <param name="productCode">string representing the products code</param>
        /// <returns>nullable IProduct, should be checked for null in calling code</returns>
        private IProduct? GetProductPricing(string productCode) => _products.FirstOrDefault(productPrice => productPrice.Code.ToUpperInvariant() == productCode);
    }
}
