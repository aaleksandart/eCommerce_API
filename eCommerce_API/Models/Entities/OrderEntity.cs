using eCommerce_API.Models.SupportModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce_API.Models.Entities
{
    public class OrderEntity
    {
        public OrderEntity()
        {
        }

        public OrderEntity(int userId, DateTime createdDate)
        {
            UserId = userId;
            CreatedDate = createdDate;
        }

        //public OrderEntity(int userId, UserEntity? user, DateTime createdDate)
        //{
        //    UserId = userId;
        //    User = user;
        //    CreatedDate = createdDate;
        //}

        public OrderEntity(List<OrderLineEntity> orderLines, DateTime createdDate)
        {
            OrderLines = orderLines;
            CreatedDate = createdDate;
        }

        public OrderEntity(List<OrderLineEntity> orderLines, DateTime createdDate, int userId, string? customerName, string? customerEmail, string? customerPhoneNumber, string? customerStreetName, string? customerPostalCode, string? customerCity, string? customerCountry)
        {
            OrderLines = orderLines;
            CreatedDate = createdDate;
            UserId = userId;

            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhoneNumber = customerPhoneNumber;
            CustomerStreetName = customerStreetName;
            CustomerPostalCode = customerPostalCode;
            CustomerCity = customerCity;
            CustomerCountry = customerCountry;
        }

        public OrderEntity(List<OrderLineEntity> orderLines, DateTime createdDate, int userId, Status orderState, string? customerName, string? customerEmail, string? customerPhoneNumber, string? customerStreetName, string? customerPostalCode, string? customerCity, string? customerCountry)
        {
            OrderLines = orderLines;
            CreatedDate = createdDate;
            UserId = userId;
            OrderState = orderState;

            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhoneNumber = customerPhoneNumber;
            CustomerStreetName = customerStreetName;
            CustomerPostalCode = customerPostalCode;
            CustomerCity = customerCity;
            CustomerCountry = customerCountry;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public List<OrderLineEntity> OrderLines { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; private set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public Status OrderState { get; set; }


        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? CustomerName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? CustomerEmail { get; set; }
        [Required]
        public string? CustomerPhoneNumber { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string? CustomerStreetName { get; set; }
        [Required]
        [Column(TypeName = "varchar(5)")]
        public string? CustomerPostalCode { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? CustomerCity { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? CustomerCountry { get; set; }

        public enum Status
        {
            Created,
            Active,
            Finished
        }
        public void PriceCalculator(List<OrderLineEntity> orderLines)
        {
            decimal totalPrice = 0;
            foreach (var orderLine in orderLines)
                totalPrice += orderLine.Price * orderLine.Quantity;

            TotalPrice = totalPrice;
        }

    }
}
