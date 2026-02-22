using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MenStyle.Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Price")]
        [Range(0.01, 99999.99)]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [Display(Name = "Stock Quantity")]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Display(Name = "Is Featured")]
        public bool IsFeatured { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}
