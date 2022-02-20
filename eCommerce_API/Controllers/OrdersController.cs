#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce_API;
using eCommerce_API.Models.Entities;
using eCommerce_API.Models.DisplayModels;
using eCommerce_API.Models.SupportModels;
using eCommerce_API.Models.CreateModels;
using eCommerce_API.Models.UpdateModels;
using eCommerce_API.Services;
using Microsoft.AspNetCore.Authorization;
using eCommerce_API.Filters;

namespace eCommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [UserAccessApiKey]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) =>
            _orderService = orderService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDisplayModel>>> GetOrders() =>
            await _orderService.GetOrdersAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDisplayModel>> GetOrder(int id) =>
            await _orderService.GetOrderAsync(id);

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderUpdateModel updateOrder) =>
            await _orderService.UpdateOrderAsync(id, updateOrder);

        [HttpPost]
        public async Task<ActionResult<OrderDisplayModel>> CreateOrder(OrderCreateModel newOrder) =>
            await _orderService.CreateOrderAsync(newOrder);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id) =>
            await _orderService.DeleteOrderAsync(id);


    }
}
