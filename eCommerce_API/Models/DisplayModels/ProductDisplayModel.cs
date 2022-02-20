using eCommerce_API.Models.Entities;
using eCommerce_API.Models.SupportModels;

namespace eCommerce_API.Models.DisplayModels
{
    public class ProductDisplayModel
    {
        public ProductDisplayModel()
        {
        }

        public ProductDisplayModel(string? productName, string? productDescription, decimal price)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
        }

        public ProductDisplayModel(int id, Guid barcode, string? productName, string? productDescription, decimal price, string categoryName)
        {
            Id = id;
            Barcode = barcode;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            CategoryName = categoryName;
        }
        
        public int Id { get; set; }
        public Guid Barcode { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string? CategoryName { get; set; }
    }
}
