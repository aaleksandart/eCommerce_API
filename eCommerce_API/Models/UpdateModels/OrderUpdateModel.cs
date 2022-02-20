using static eCommerce_API.Models.Entities.OrderEntity;

namespace eCommerce_API.Models.UpdateModels
{
    public class OrderUpdateModel
    {
        public OrderUpdateModel(int orderstate)
        {
            OrderState = orderstate;
        }

        public int OrderState { get; set; }
        public DateTime UpdateDate { get; private set; } = DateTime.Now;

    }
}
