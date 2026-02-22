using Razorpay.Api;
using Microsoft.Extensions.Options;

namespace MenStyle.Web.Services
{
    public interface IRazorpayService
    {
        string CreateOrder(decimal amount, string orderId);
        bool VerifyPayment(string paymentId, string orderId, string signature);
    }

    public class RazorpayService : IRazorpayService
    {
        private readonly string _keyId;
        private readonly string _keySecret;

        public RazorpayService(IConfiguration configuration)
        {
            _keyId = configuration["Razorpay:KeyId"] ?? "";
            _keySecret = configuration["Razorpay:KeySecret"] ?? "";
        }

        public string CreateOrder(decimal amount, string orderId)
        {
            try {
                var client = new RazorpayClient(_keyId, _keySecret);
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", (int)(amount * 100)); // Amount in paise
                options.Add("currency", "USD"); // Using USD for this demo
                options.Add("receipt", orderId);
                
                Order order = client.Order.Create(options);
                return order["id"].ToString();
            }
            catch {
                // Return dummy ID if key is not configured for demo purposes
                return "order_dummy_" + orderId;
            }
        }

        public bool VerifyPayment(string paymentId, string orderId, string signature)
        {
            try {
                var client = new RazorpayClient(_keyId, _keySecret);
                Dictionary<string, string> attributes = new Dictionary<string, string>();
                attributes.Add("razorpay_payment_id", paymentId);
                attributes.Add("razorpay_order_id", orderId);
                attributes.Add("razorpay_signature", signature);

                Utils.verifyPaymentSignature(attributes);
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
