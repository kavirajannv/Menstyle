using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MenStyle.Web.Models;
using MenStyle.Web.Services;

namespace MenStyle.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) => _categoryService = categoryService;

        public async Task<IActionResult> Index()
            => View(await _categoryService.GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(category);
                TempData["Success"] = "Category created!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(category);
                TempData["Success"] = "Category updated!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            TempData["Success"] = "Category deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}
