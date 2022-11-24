namespace XETechTest.Core.Models
{
    /// <summary>
    /// Interface for the products handled in the total calculation
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Product code. There is an expectation that this remains unique and is a single character long 
        /// </summary>
        string Code { get; }
        
        /// <summary>
        /// The price paid for singular items of this sort.
        /// </summary>
        decimal StandardPrice { get; }

        /// <summary>
        /// A set of pricings and the corresponding quanitities required to get the associated price.
        /// </summary>
        Dictionary<int, decimal> VolumePricings { get; }

        /// <summary>
        /// takes a quanitity of items and finds the total amount the customer needs to pay.
        /// This divides the quantity into the best possible volume pricing
        /// </summary>
        /// <param name="quantity">Required, the amoint of the product that is being purchased</param>
        /// <returns>A decimal representing the total price of all products of this type purchased in the transaction</returns>
        decimal GetPrice(int quantity);
    }
}