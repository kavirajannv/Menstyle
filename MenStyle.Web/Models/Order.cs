using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenStyle.Web.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending";
        public string PaymentMethod { get; set; } = "COD"; // COD, Razorpay
        public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid, Failed
        public string? TransactionId { get; set; } // For Razorpay

        [StringLength(500)]
        [Display(Name = "Shipping Address")]
        public string? ShippingAddress { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [StringLength(20)]
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [StringLength(200)]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
