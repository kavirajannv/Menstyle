using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MenStyle.Web.Services;

namespace MenStyle.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) => _orderService = orderService;

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetUserOrdersAsync(GetUserId());
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null || order.UserId != GetUserId())
                return NotFound();
            return View(order);
        }
    }
}
