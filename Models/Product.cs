using System.ComponentModel.DataAnnotations;
namespace Inventory.Models
{
    public class Product
    {

        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter Product Name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = default!;

        [Required(ErrorMessage = "Please enter a valid SKU")]
        public string SKU { get; set; } = default!;

        [Required(ErrorMessage = "Please enter the stock quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; } = default!;

        [Required(ErrorMessage = "Please enter the product price")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public int Price { get; set; } = default!;

        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Category")]
        public string Category { get; set; } = default!;
    }
}
