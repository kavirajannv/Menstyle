using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MenStyle.Web.Models.ViewModels;
using MenStyle.Web.Services;
using MenStyle.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace MenStyle.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IOrderService _orderService;
        private readonly ApplicationDbContext _context;

        public DashboardController(IProductService productService, ICategoryService categoryService,
            IOrderService orderService, ApplicationDbContext context)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var orders = await _orderService.GetAllOrdersAsync();
            var salesByMonth = await _orderService.GetSalesByMonthAsync();

            var vm = new AdminDashboardViewModel
            {
                TotalProducts = products.Count(),
                TotalCategories = categories.Count(),
                TotalOrders = orders.Count(),
                TotalRevenue = orders.Where(o => o.Status != "Cancelled").Sum(o => o.TotalAmount),
                RecentOrders = orders.Take(10),
                SalesByMonth = salesByMonth
            };

            return View(vm);
        }
    }
}
