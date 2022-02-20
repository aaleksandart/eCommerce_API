using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce_API.Models.Entities
{
    public class ProductEntity
    {
        public ProductEntity()
        {
        }

        public ProductEntity(string? productName, string? productDescription, decimal price)
        {
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
        }

        public ProductEntity(Guid barcode, string? productName, string? productDescription, decimal price, int categoryId)
        {
            Barcode = barcode;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            CategoryId = categoryId;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public Guid Barcode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? ProductName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string? ProductDescription { get; set; }
        
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }


        [Required]
        public int CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }
    }
}
