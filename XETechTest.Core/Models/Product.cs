using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XETechTest.Core.Models
{
    /// <summary>
    /// Standard implementation of the IProduct interface. represents a product purchased in a single transaction
    /// </summary>
    public class Product : IProduct
    {
        /// <summary>
        /// Product code. There is an expectation that this remains unique and is a single character long 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Standard pricing is used where no matches for given quantity arent found in the VolumePricings prop
        /// </summary>
        public decimal StandardPrice { get; set; }

        /// <summary>
        /// A dictionary that represent the volume pricing. Key is the quantity of the product and value is the new pricing 
        /// to use instead of the standard pricing
        /// </summary>
        public Dictionary<int, decimal> VolumePricings { get; set; }


        public decimal GetPrice(int quantity)
        {
            // escape condition for both erroneous method calls with zero quantity as well as escaping recursive method calls
            if (quantity == 0)
                return 0M;

            // we attempt to get the volume first volume price that has a lower quantity requirement than the current quantity.
            // then we take the highest quantity pricing.
            var volumePricingModel = VolumePricings
                .Where(pricing => pricing.Key <= quantity)
                .OrderByDescending(price => price.Key)
                .FirstOrDefault();

            // if the above line has returned a default key value pair, there was no applicable volume pricing
            if (volumePricingModel.Equals(default(KeyValuePair<int, decimal>)))
            {
                // use the standard pricing multiplied by the remaing quantity
                return StandardPrice * quantity;
            }
            else
            {
                // if volume pricing was applicable, we return the volume pricing value plus the result
                // of recursively calling this function with the quantity reduced from the useage of the preceeding volume pricing
                return volumePricingModel.Value + GetPrice(quantity - volumePricingModel.Key);
            }
        }
    }
}
