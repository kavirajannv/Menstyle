using MenStyle.Web.Models;

namespace MenStyle.Web.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> PlaceOrderAsync(string userId, string fullName, string address, string city, string postalCode, string phone, string paymentMethod);
        Task UpdateStatusAsync(int orderId, string status);
        Task UpdatePaymentStatusAsync(int orderId, string paymentStatus, string? transactionId = null);
        Task<Dictionary<string, decimal>> GetSalesByMonthAsync();
    }
}
