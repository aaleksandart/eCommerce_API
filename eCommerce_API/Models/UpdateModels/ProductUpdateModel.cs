namespace eCommerce_API.Models.UpdateModels
{
    public class ProductUpdateModel
    {
        public ProductUpdateModel()
        {
        }

        public ProductUpdateModel(string? productName, string? productDescription, decimal price, string? categoryName)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            CategoryName = categoryName;
        }

        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string? CategoryName { get; set; }
    }
}
