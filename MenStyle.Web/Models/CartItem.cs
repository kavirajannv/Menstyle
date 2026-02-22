using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenStyle.Web.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [NotMapped]
        public decimal Subtotal => Product != null ? Product.Price * Quantity : 0;
    }
}
