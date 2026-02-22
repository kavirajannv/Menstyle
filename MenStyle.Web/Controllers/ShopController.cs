using Microsoft.AspNetCore.Mvc;
using MenStyle.Web.Models.ViewModels;
using MenStyle.Web.Services;

namespace MenStyle.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ShopController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? search, int? categoryId)
        {
            var categories = await _categoryService.GetAllAsync();
            var vm = new ProductsViewModel
            {
                Categories = categories,
                SearchQuery = search,
                CategoryId = categoryId
            };

            if (!string.IsNullOrWhiteSpace(search))
            {
                vm.Products = await _productService.SearchAsync(search);
            }
            else if (categoryId.HasValue)
            {
                vm.Products = await _productService.GetByCategoryAsync(categoryId.Value);
                var cat = categories.FirstOrDefault(c => c.Id == categoryId.Value);
                vm.CategoryName = cat?.Name;
            }
            else
            {
                vm.Products = await _productService.GetAllAsync();
            }

            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
