using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MenStyle.Web.Models;
using MenStyle.Web.Services;

namespace MenStyle.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
            => View(await _productService.GetAllAsync());

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateAsync(product);
                TempData["Success"] = "Product created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(product);
                TempData["Success"] = "Product updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            TempData["Success"] = "Product deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
