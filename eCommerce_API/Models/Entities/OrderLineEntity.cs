using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce_API.Models.Entities
{
    public class OrderLineEntity
    {
        public OrderLineEntity()
        {
        }

        public OrderLineEntity(ProductEntity? product, int quantity, decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }

        public OrderLineEntity(int orderId, int productId, int quantity, decimal price)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        [Key]
        public int Id { get; set; }

        
        public int OrderId { get; set; }
        public OrderEntity? Order { get; set; }
        
        
        public int ProductId { get; set; }
        public ProductEntity? Product { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
