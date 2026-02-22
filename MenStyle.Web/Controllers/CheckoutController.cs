using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using MenStyle.Web.Models.ViewModels;
using MenStyle.Web.Services;

namespace MenStyle.Web.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IRazorpayService _razorpayService;
        private readonly IConfiguration _configuration;

        public CheckoutController(IOrderService orderService, ICartService cartService, IRazorpayService razorpayService, IConfiguration configuration)
        {
            _orderService = orderService;
            _cartService = cartService;
            _razorpayService = razorpayService;
            _configuration = configuration;
        }

        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var cartItems = await _cartService.GetCartItemsAsync(userId);

            if (!cartItems.Any())
                return RedirectToAction("Index", "Cart");

            var vm = new CheckoutViewModel
            {
                CartItems = cartItems.ToList(),
                Total = await _cartService.GetCartTotalAsync(userId),
                FullName = User.Identity?.Name ?? string.Empty
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {
            var userId = GetUserId();

            if (!ModelState.IsValid)
            {
                model.CartItems = (await _cartService.GetCartItemsAsync(userId)).ToList();
                model.Total = await _cartService.GetCartTotalAsync(userId);
                return View("Index", model);
            }

            try
            {
                var order = await _orderService.PlaceOrderAsync(
                    userId, model.FullName, model.ShippingAddress,
                    model.City, model.PostalCode, model.Phone, model.PaymentMethod);

                if (model.PaymentMethod == "Razorpay")
                {
                    return RedirectToAction("Payment", new { orderId = order.Id });
                }

                return RedirectToAction("Success", new { orderId = order.Id });
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }

        public async Task<IActionResult> Success(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null || order.UserId != GetUserId())
                return NotFound();

            return View(order);
        }

        public async Task<IActionResult> Payment(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null || order.UserId != GetUserId())
                return NotFound();

            var razorpayOrderId = _razorpayService.CreateOrder(order.TotalAmount, order.Id.ToString());
            
            ViewBag.RazorpayOrderId = razorpayOrderId;
            ViewBag.RazorpayKey = _configuration["Razorpay:KeyId"];
            ViewBag.Amount = (int)(order.TotalAmount * 100);
            
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyPayment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature, int orderId)
        {
            var isValid = _razorpayService.VerifyPayment(razorpay_payment_id, razorpay_order_id, razorpay_signature);
            if (isValid)
            {
                await _orderService.UpdatePaymentStatusAsync(orderId, "Paid", razorpay_payment_id);
                return RedirectToAction("Success", new { orderId });
            }

            TempData["Error"] = "Payment verification failed.";
            return RedirectToAction("Index", "Cart");
        }
    }
}
