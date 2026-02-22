using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MenStyle.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
