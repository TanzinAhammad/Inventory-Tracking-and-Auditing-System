using System.ComponentModel.DataAnnotations;
namespace CRUD_Using_Repository.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = default!;
        public string Gender { get; set; } = default!;
        public string Email { get; set; } = default!;
        [Display(Name="Pin Code")]
        public int PinCode { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
