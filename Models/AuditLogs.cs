using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Using_Repository.Models
{
    public class AuditLogs
    {
        [Key]
        public int AuditId { get; set; }
        public int SKU { get; set; }
        [ForeignKey("SKU")]
        public string Product_Name { get; set; } = default!;
        public DateTime TimeStamp { get; set; }
        public string ChangeType { get; set; } = default!;
        public int Quantity { get; set; }
        public string UserName { get; set; } = default!;

    }
}
