using Microsoft.EntityFrameworkCore;
using MenStyle.Web.Data;
using MenStyle.Web.Models;

namespace MenStyle.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await _context.Products.Include(p => p.Category).ToListAsync();

        public async Task<IEnumerable<Product>> GetFeaturedAsync()
            => await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsFeatured && p.IsActive)
                .ToListAsync();

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
            => await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .ToListAsync();

        public async Task<IEnumerable<Product>> SearchAsync(string query)
        {
            var lower = query.ToLower();
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsActive && (
                    p.Name.ToLower().Contains(lower) ||
                    (p.Description != null && p.Description.ToLower().Contains(lower)) ||
                    (p.Category != null && p.Category.Name.ToLower().Contains(lower))))
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
            => await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public async Task CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Products.AnyAsync(p => p.Id == id);
    }
}
