using eCommerce_API.Models.CreateModels;
using eCommerce_API.Models.DisplayModels;
using eCommerce_API.Models.Entities;
using eCommerce_API.Models.SupportModels;
using eCommerce_API.Models.UpdateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_API.Services
{
    public interface IOrderService
    {
        Task<List<OrderDisplayModel>> GetOrdersAsync();
        Task<ActionResult<OrderDisplayModel>> GetOrderAsync(int id);
        Task<IActionResult> UpdateOrderAsync(int id, OrderUpdateModel updateOrder);
        Task<ActionResult<OrderDisplayModel>> CreateOrderAsync(OrderCreateModel createOrder);
        Task<IActionResult> DeleteOrderAsync(int id);
    }
    public class OrderService : ControllerBase, IOrderService
    {
        private readonly SqlContext _context;

        public OrderService(SqlContext context)
        {
            _context = context;
        }
        public async Task<List<OrderDisplayModel>> GetOrdersAsync()
        {
            UserDisplayModel user = new();
            List<OrderDisplayModel> orders = new();

            var orderEntities = await _context.Orders
                .Include(x => x.OrderLines)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .ToListAsync();

            foreach (var orderEntity in orderEntities)
            {
                List<OrderLineModel> orderlines = new();

                foreach (var line in orderEntity.OrderLines)
                {
                    if (line.OrderId == orderEntity.Id)
                    {
                        var product = new ProductDisplayModel(
                        line.Product.Id,
                        line.Product.Barcode,
                        line.Product.ProductName,
                        line.Product.ProductDescription,
                        line.Product.Price,
                        line.Product.Category.CategoryName);

                        orderlines.Add(new OrderLineModel(
                            product,
                            line.Price,
                            line.Quantity));
                    }
                }
                orders.Add(new OrderDisplayModel(
                    orderEntity.Id,
                    orderEntity.CustomerName,
                    orderEntity.CustomerEmail,
                    orderEntity.CustomerPhoneNumber,
                    orderEntity.CustomerStreetName,
                    orderEntity.CustomerPostalCode,
                    orderEntity.CustomerCity,
                    orderEntity.CustomerCountry,
                    orderlines,
                    orderEntity.TotalPrice,
                    orderEntity.CreatedDate,
                    orderEntity.UpdatedDate,
                    orderEntity.OrderState.ToString()));
            }
            return orders;
        }

        public async Task<ActionResult<OrderDisplayModel>> GetOrderAsync(int id)
        {
            List<OrderLineModel> orderlines = new List<OrderLineModel>();
            var order = await _context.Orders
                .Include(x => x.OrderLines)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (order == null)
                return NotFound();

            foreach (var line in order.OrderLines)
            {
                var product = new ProductDisplayModel(
                    line.Product.Id,
                    line.Product.Barcode,
                    line.Product.ProductName,
                    line.Product.ProductDescription,
                    line.Product.Price,
                    line.Product.Category.CategoryName);

                orderlines.Add(new OrderLineModel(
                    product,
                    line.Price,
                    line.Quantity));
            }
            return Ok(new OrderDisplayModel(
                order.Id,
                order.CustomerName,
                order.CustomerEmail,
                order.CustomerPhoneNumber,
                order.CustomerStreetName,
                order.CustomerPostalCode,
                order.CustomerCity,
                order.CustomerCountry,
                orderlines,
                order.TotalPrice,
                order.CreatedDate,
                order.UpdatedDate,
                order.OrderState.ToString()));
        }

        public async Task<IActionResult> UpdateOrderAsync(int id, OrderUpdateModel updateOrder)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
                return BadRequest("An order with that ID dont exist.");

            if (updateOrder.OrderState != 0 && updateOrder.OrderState != 1 && updateOrder.OrderState != 2)
                return BadRequest("Input info was incorrect:  0 = Created, 1 = Active, 2 = Finished");
            else
                existingOrder.OrderState = (OrderEntity.Status)updateOrder.OrderState;

            existingOrder.UpdatedDate = DateTime.Now;
            _context.Entry(existingOrder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<ActionResult<OrderDisplayModel>> CreateOrderAsync(OrderCreateModel newOrder)
        {
            OrderEntity order = new OrderEntity();
            List<OrderLineEntity> lines = new List<OrderLineEntity>();
            if (newOrder.UserId == 0 || newOrder.OrderLines.Count == 0)
                return BadRequest("Input info was incorrect.");

            foreach (var line in newOrder.OrderLines)
            {
                var product = await _context.Products.FindAsync(line.ProductId);
                if (product != null)
                {
                    var orderLine = new OrderLineEntity(
                        order.Id,
                        line.ProductId,
                        line.Quantity,
                        product.Price);

                    lines.Add(orderLine);
                    await _context.OrderLines.AddAsync(orderLine);
                }
            }
            var user = await _context.Users
                .Include(x => x.ContactInfo)
                .Include(x => x.Address)
                .Where(x => x.Id == newOrder.UserId)
                .FirstOrDefaultAsync();

            if (user == null)
                return BadRequest("A user with that ID dont exist.");

            order = new(
                lines,
                newOrder.CreatedDate,
                user.Id,
                OrderEntity.Status.Created,
                $"{user.FirstName} {user.LastName}",
                user.Email,
                user.ContactInfo.PhoneNumber,
                user.Address.Streetname,
                user.Address.PostalCode,
                user.Address.City,
                user.Address.Country);

            order.PriceCalculator(order.OrderLines);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderCreated = await _context.Orders
                .Include(x => x.OrderLines)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .Where(x => x.Id == order.Id)
                .FirstOrDefaultAsync();

            List<OrderLineModel> orderlines = new List<OrderLineModel>();
            foreach (var line in orderCreated.OrderLines)
            {
                var product = new ProductDisplayModel(
                    line.Product.Id,
                    line.Product.Barcode,
                    line.Product.ProductName,
                    line.Product.ProductDescription,
                    line.Product.Price,
                    line.Product.Category.CategoryName);

                orderlines.Add(new OrderLineModel(
                    product,
                    line.Price,
                    line.Quantity));
            }
            OrderDisplayModel displayOrder = new OrderDisplayModel(
                order.Id,
                order.CustomerName,
                order.CustomerEmail,
                order.CustomerPhoneNumber,
                order.CustomerStreetName,
                order.CustomerPostalCode,
                order.CustomerCity,
                order.CustomerCountry,
                orderlines,
                order.TotalPrice,
                order.CreatedDate,
                order.UpdatedDate,
                order.OrderState.ToString());

            return CreatedAtAction("GetOrder", new { id = displayOrder.Id }, displayOrder);
        }

        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            var orderDelete = await _context.Orders.FindAsync(id);
            if (orderDelete == null)
                return BadRequest("An order with that ID dont exist.");

            _context.Orders.Remove(orderDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
