using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MenStyle.Web.Models;
using MenStyle.Web.Models.ViewModels;
using MenStyle.Web.Services;

namespace MenStyle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                FeaturedProducts = await _productService.GetFeaturedAsync(),
                Categories = await _categoryService.GetAllAsync()
            };
            return View(vm);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
