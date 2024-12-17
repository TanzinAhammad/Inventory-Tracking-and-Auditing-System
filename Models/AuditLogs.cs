using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    public class AuditLogs
    {
        [Key]
        public int AuditId { get; set; }

        // Foreign Key to Product
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = default!;

        [Required]
        [Display(Name = "Timestamp")]
        public DateTime TimeStamp { get; set; }

        [Required(ErrorMessage = "Change Type is required")]
        [Display(Name = "Change Type")]
        public string ChangeType { get; set; } = default!;

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [Display(Name = "User")]
        public string UserName { get; set; } = default!;

    }
}
