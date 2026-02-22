using Microsoft.EntityFrameworkCore;
using MenStyle.Web.Data;
using MenStyle.Web.Hubs;
using MenStyle.Web.Models;
using Microsoft.AspNetCore.SignalR;

namespace MenStyle.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        private readonly IHubContext<OrderHub> _hubContext;

        public OrderService(ApplicationDbContext context, ICartService cartService, IHubContext<OrderHub> hubContext)
        {
            _context = context;
            _cartService = cartService;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
            => await _context.Orders
                .Include(o => o.OrderDetails).ThenInclude(d => d.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
            => await _context.Orders
                .Include(o => o.OrderDetails).ThenInclude(d => d.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

        public async Task<Order?> GetByIdAsync(int id)
            => await _context.Orders
                .Include(o => o.OrderDetails).ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Order> PlaceOrderAsync(string userId, string fullName, string address, string city, string postalCode, string phone, string paymentMethod)
        {
            var cartItems = (await _cartService.GetCartItemsAsync(userId)).ToList();
            if (!cartItems.Any())
                throw new InvalidOperationException("Cart is empty.");

            var order = new Order
            {
                UserId = userId,
                FullName = fullName,
                ShippingAddress = address,
                City = city,
                PostalCode = postalCode,
                Phone = phone,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                PaymentMethod = paymentMethod,
                PaymentStatus = "Pending",
                TotalAmount = cartItems.Sum(i => i.Subtotal)
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Notify Admin in real-time
            await _hubContext.Clients.All.SendAsync("ReceiveOrderNotification", $"New Order #{order.Id} placed by {fullName}!", order.Id);

            foreach (var item in cartItems)
            {
                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product!.Price
                });

                // Reduce stock
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                    product.StockQuantity = Math.Max(0, product.StockQuantity - item.Quantity);
            }

            await _context.SaveChangesAsync();
            await _cartService.ClearCartAsync(userId);

            return order;
        }

        public async Task UpdateStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                await _context.SaveChangesAsync();

                // Notify user/admin about status update
                await _hubContext.Clients.All.SendAsync("ReceiveOrderNotification", $"Order #{orderId} status updated to {status}.", orderId);
            }
        }

        public async Task UpdatePaymentStatusAsync(int orderId, string paymentStatus, string? transactionId = null)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.PaymentStatus = paymentStatus;
                if (!string.IsNullOrEmpty(transactionId))
                {
                    order.TransactionId = transactionId;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Dictionary<string, decimal>> GetSalesByMonthAsync()
        {
            var orders = await _context.Orders
                .Where(o => o.Status != "Cancelled")
                .ToListAsync();

            return orders
                .GroupBy(o => o.OrderDate.ToString("MMM yyyy"))
                .ToDictionary(g => g.Key, g => g.Sum(o => o.TotalAmount));
        }
    }
}
