namespace eCommerce_API.Models.CreateModels
{
    public class OrderLineCreateModel
    {
        public OrderLineCreateModel()
        {
        }

        public OrderLineCreateModel(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
