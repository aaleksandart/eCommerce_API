using eCommerce_API.Models.Entities;

namespace eCommerce_API.Models.CreateModels
{
    public class ProductCreateModel
    {
        public ProductCreateModel()
        {
        }

        public ProductCreateModel(string? productName, string? productDescription, decimal price, string categoryName)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            CategoryName = categoryName;
        }

        public Guid Barcode { get; private set; } = Guid.NewGuid();
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string? CategoryName { get; set; }
    }
}
