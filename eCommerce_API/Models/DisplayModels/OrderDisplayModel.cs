using eCommerce_API.Models.CreateModels;
using eCommerce_API.Models.Entities;
using eCommerce_API.Models.SupportModels;
using static eCommerce_API.Models.Entities.OrderEntity;

namespace eCommerce_API.Models.DisplayModels
{
    public class OrderDisplayModel
    {
        public OrderDisplayModel()
        {
        }

        public OrderDisplayModel(int id, List<OrderLineModel> orderLines, decimal totalPrice, string? orderState)
        {
            Id = id;
            OrderLines = orderLines;
            TotalPrice = totalPrice;
            OrderState = orderState;
        }

        public OrderDisplayModel(
            int id, 
            string? customerName, 
            string? customerEmail, 
            string? customerPhoneNumber, 
            string? customerStreetName, 
            string? customerPostalCode, 
            string? customerCity, 
            string? customerCountry, 
            List<OrderLineModel> orderLines, 
            decimal totalPrice, 
            DateTime createdDate,
            DateTime updatedDate,
            string? orderState)
        {
            Id = id;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhoneNumber = customerPhoneNumber;
            CustomerStreetName = customerStreetName;
            CustomerPostalCode = customerPostalCode;
            CustomerCity = customerCity;
            CustomerCountry = customerCountry;
            OrderLines = orderLines;
            TotalPrice = totalPrice;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            OrderState = orderState;
        }

        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public string? CustomerStreetName { get; set; }
        public string? CustomerPostalCode { get; set; }
        public string? CustomerCity { get; set; }
        public string? CustomerCountry { get; set; }
        public List<OrderLineModel> OrderLines { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? OrderState { get; set; }
    }
}