using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        { 

        }
        public DbSet<Product>Products { get; set; }
        public DbSet<AuditLogs>AuditLogs { get; set; }
        
    }
}
