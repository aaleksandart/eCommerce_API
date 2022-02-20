using eCommerce_API.Models.DisplayModels;
using eCommerce_API.Models.Entities;

namespace eCommerce_API.Models.SupportModels
{
    public class OrderLineModel
    {
        public OrderLineModel()
        {
        }

        public OrderLineModel(ProductDisplayModel? product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;

        }
        public ProductDisplayModel? Product { get; set; }
        
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
