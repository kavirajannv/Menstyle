using Microsoft.EntityFrameworkCore;
using MenStyle.Web.Data;
using MenStyle.Web.Models;

namespace MenStyle.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _context.Categories.Include(c => c.Products).ToListAsync();

        public async Task<Category?> GetByIdAsync(int id)
            => await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);

        public async Task CreateAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat != null) { _context.Categories.Remove(cat); await _context.SaveChangesAsync(); }
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Categories.AnyAsync(c => c.Id == id);
    }
}
