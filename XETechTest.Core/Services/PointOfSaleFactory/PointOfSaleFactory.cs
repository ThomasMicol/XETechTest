using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XETechTest.Core.Models;

namespace XETechTest.Core.Services.PointOfSaleFactory
{
    /// <summary>
    /// Implementation of the IPointOfSaleFactory. Benefit of this is that client code that relies on the result of the CreatePointOfSaleService 
    /// Method is that the implementation detail of this class can be swapped out completely to instead, for example, get the products from an API
    /// without having to touch the implementation of any of the client code. 
    /// </summary>
    public class PointOfSaleFactory : IPointOfSaleFactory
    {
        /// <summary>
        /// For the purposes of this tech test we just need a static list of these products
        /// </summary>
        private readonly IList<IProduct> _staticProducts = new List<IProduct>
        {
            new Product
            {
                Code = "A",
                StandardPrice = 1.25M,
                VolumePricings = new Dictionary<int, decimal>
                {
                    { 3, 3.00M }
                }
            },
            new Product
            {
                Code = "B",
                StandardPrice = 4.25M,
                VolumePricings = new Dictionary<int, decimal> { }
            },
            new Product
            {
                Code = "C",
                StandardPrice = 1.00M,
                VolumePricings = new Dictionary<int, decimal>
                {
                    { 6, 5.00M }
                }
            },
            new Product
            {
                Code = "D",
                StandardPrice = 0.75M,
                VolumePricings = new Dictionary<int, decimal> { }
            },
        };

        /// <summary>
        /// Gets a list of products, currently just returns the static product list. In a real implementation this might instead
        /// perform retreive data from the database or apply business logic on which products are available in certain regions
        /// </summary>
        /// <returns>List of IProducts</returns>
        protected IList<IProduct> GetProducts() => _staticProducts;

        public IPointOfSaleService CreatePointOfSaleService()
        {
            var products = GetProducts();

            var pointOfSaleService = new PointOfSaleService(products);

            return pointOfSaleService;
        }
    }
}
