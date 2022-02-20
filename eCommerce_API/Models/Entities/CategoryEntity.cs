using System.ComponentModel.DataAnnotations;

namespace eCommerce_API.Models.Entities
{
    public class CategoryEntity
    {
        public CategoryEntity()
        {
        }

        public CategoryEntity(string? categoryName)
        {
            CategoryName = categoryName;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string? CategoryName { get; set; }
    }
}
