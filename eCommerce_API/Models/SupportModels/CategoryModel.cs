namespace eCommerce_API.Models.SupportModels
{
    public class CategoryModel
    {
        public CategoryModel()
        {
        }

        public CategoryModel(string? categoryName)
        {
            CategoryName = categoryName;
        }

        public string? CategoryName { get; set; }
    }
}
