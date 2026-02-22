using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MenStyle.Web.Models.ViewModels;
using MenStyle.Web.Services;

namespace MenStyle.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var vm = new CartViewModel
            {
                Items = await _cartService.GetCartItemsAsync(userId),
                Total = await _cartService.GetCartTotalAsync(userId)
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int productId, int quantity = 1)
        {
            await _cartService.AddToCartAsync(GetUserId(), productId, quantity);
            TempData["Success"] = "Item added to cart!";
            return RedirectToAction("Index", "Shop");
        }

        [HttpPost]
        public async Task<IActionResult> AddFromDetails(int productId, int quantity = 1)
        {
            await _cartService.AddToCartAsync(GetUserId(), productId, quantity);
            TempData["Success"] = "Item added to cart!";
            return RedirectToAction("Details", "Shop", new { id = productId });
        }

        [HttpPost]
        public async Task<IActionResult> Update(int cartItemId, int quantity)
        {
            await _cartService.UpdateQuantityAsync(GetUserId(), cartItemId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            await _cartService.RemoveFromCartAsync(GetUserId(), cartItemId);
            TempData["Success"] = "Item removed from cart.";
            return RedirectToAction("Index");
        }
    }
}
