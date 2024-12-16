using System.ComponentModel.DataAnnotations;
namespace CRUD_Using_Repository.Models
{
    public class User
    {
        [Key]
        public int SKU { get; set; }
        [Required(ErrorMessage ="Please enter Product Name")]
        public string Product_Name { get; set; } = default!;
        [Required]
        public int Stock { get; set; } = default!;
        [Required]
        public int Price { get; set; } = default!;
        [Display(Name = "Category")]
        [Required]
        public string Category { get; set; }
    }
}
