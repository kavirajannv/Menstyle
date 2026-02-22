using MenStyle.Web.Models;

namespace MenStyle.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; } = new List<Product>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }

    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public string? SearchQuery { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    public class CartViewModel
    {
        public IEnumerable<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal Total { get; set; }
    }

    public class CheckoutViewModel
    {
        public string FullName { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<CartItem> CartItems { get; set; } = new();
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; } = "COD"; // Selected method
    }

    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalCategories { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public IEnumerable<Order> RecentOrders { get; set; } = new List<Order>();
        public Dictionary<string, decimal> SalesByMonth { get; set; } = new Dictionary<string, decimal>();
    }
}
