using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MenStyle.Web.Services;

namespace MenStyle.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService) => _orderService = orderService;

        public async Task<IActionResult> Index(string? search, string? status)
        {
            var orders = await _orderService.GetAllOrdersAsync();

            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(o => 
                    o.Id.ToString().Contains(search) || 
                    o.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    o.Phone.Contains(search)
                );
            }

            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                orders = orders.Where(o => o.Status == status);
            }

            ViewBag.Search = search;
            ViewBag.Status = status;

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            await _orderService.UpdateStatusAsync(id, status);
            TempData["Success"] = $"Order #{id} status updated to {status}.";
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
