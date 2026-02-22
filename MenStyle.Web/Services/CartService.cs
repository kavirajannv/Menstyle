using Microsoft.EntityFrameworkCore;
using MenStyle.Web.Data;
using MenStyle.Web.Models;

namespace MenStyle.Web.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId)
            => await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

        public async Task<int> GetCartCountAsync(string userId)
            => await _context.CartItems.Where(c => c.UserId == userId).SumAsync(c => c.Quantity);

        public async Task AddToCartAsync(string userId, int productId, int quantity = 1)
        {
            var existing = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuantityAsync(string userId, int cartItemId, int quantity)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);

            if (item != null)
            {
                if (quantity <= 0)
                    _context.CartItems.Remove(item);
                else
                    item.Quantity = quantity;

                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromCartAsync(string userId, int cartItemId)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string userId)
        {
            var items = _context.CartItems.Where(c => c.UserId == userId);
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetCartTotalAsync(string userId)
        {
            var items = await _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();
            return items.Sum(i => i.Subtotal);
        }
    }
}
