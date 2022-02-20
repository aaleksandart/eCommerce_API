using eCommerce_API.Models.Entities;
using eCommerce_API.Models.SupportModels;
using static eCommerce_API.Models.Entities.OrderEntity;

namespace eCommerce_API.Models.CreateModels
{
    public class OrderCreateModel
    {
        public OrderCreateModel()
        {
        }

        public OrderCreateModel(int userId, List<OrderLineCreateModel>? orderLines)
        {
            UserId = userId;
            OrderLines = orderLines;
        }

        public int UserId { get; set; }
        public List<OrderLineCreateModel>? OrderLines { get; set; }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public DateTime UpdateDate { get; private set; } = DateTime.Now;
    }
}