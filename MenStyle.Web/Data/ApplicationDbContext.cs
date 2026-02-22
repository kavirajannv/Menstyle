using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MenStyle.Web.Models;

namespace MenStyle.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Shirts", Description = "Formal and casual shirts" },
                new Category { Id = 2, Name = "Trousers", Description = "Classic and modern trousers" },
                new Category { Id = 3, Name = "Suits", Description = "Complete suit sets" },
                new Category { Id = 4, Name = "Jackets", Description = "Blazers and jackets" },
                new Category { Id = 5, Name = "Accessories", Description = "Ties, belts, and more" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Classic White Dress Shirt", Description = "Premium Egyptian cotton dress shirt, perfect for formal occasions.", Price = 59.99m, CategoryId = 1, StockQuantity = 50, IsFeatured = true, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1598033129183-c4f50c736f10?w=600" },
                new Product { Id = 2, Name = "Oxford Blue Slim Fit Shirt", Description = "Modern slim-fit Oxford shirt, ideal for business casual settings.", Price = 49.99m, CategoryId = 1, StockQuantity = 40, IsFeatured = true, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1563630423918-b58f07336ac9?w=600" },
                new Product { Id = 3, Name = "Charcoal Wool Trousers", Description = "Tailored charcoal wool trousers with a flat front design.", Price = 89.99m, CategoryId = 2, StockQuantity = 30, IsFeatured = true, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1541099649105-f69ad21f3246?w=600" },
                new Product { Id = 4, Name = "Navy Blue Chinos", Description = "Smart casual navy chinos made from stretch cotton blend.", Price = 64.99m, CategoryId = 2, StockQuantity = 45, IsFeatured = false, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1473966968600-fa801b869a1a?w=600" },
                new Product { Id = 5, Name = "Black Peak Lapel Suit", Description = "Sophisticated three-piece black suit with peak lapels.", Price = 299.99m, CategoryId = 3, StockQuantity = 20, IsFeatured = true, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1594938298603-c8148c4b0e94?w=600" },
                new Product { Id = 6, Name = "Grey Herringbone Suit", Description = "Classic grey herringbone two-piece suit for the modern gentleman.", Price = 249.99m, CategoryId = 3, StockQuantity = 15, IsFeatured = true, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=600" },
                new Product { Id = 7, Name = "Camel Wool Blazer", Description = "Premium camel wool blazer that elevates any outfit.", Price = 179.99m, CategoryId = 4, StockQuantity = 25, IsFeatured = true, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?w=600" },
                new Product { Id = 8, Name = "Navy Linen Blazer", Description = "Lightweight navy linen blazer for warm weather elegance.", Price = 159.99m, CategoryId = 4, StockQuantity = 30, IsFeatured = false, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1548705085-101177834f47?w=600" },
                new Product { Id = 9, Name = "Silk Tie Collection", Description = "Set of 3 premium silk ties in classic patterns.", Price = 79.99m, CategoryId = 5, StockQuantity = 60, IsFeatured = false, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1589756823695-278bc923f962?w=600" },
                new Product { Id = 10, Name = "Leather Belt Set", Description = "Genuine leather belt with reversible buckle in black and brown.", Price = 44.99m, CategoryId = 5, StockQuantity = 70, IsFeatured = false, IsActive = true, ImageUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=600" }
            );
        }
    }
}
