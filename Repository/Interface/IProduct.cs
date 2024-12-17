
using Inventory.Models;

namespace Inventory.Repository.Interface
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> GetProducts();
        //void AddProduct(Product product);
        Task<int> AddProduct(Product product);

        Task<Product> GetProductById(int id);
        Task<bool> UpdateRecord(Product product);

        Task<bool> DeleteRecord(int id);

        Task<int> UpdateAuditLogs(AuditLogs auditlogs);

        Task<IEnumerable<AuditLogs>> Audits();

       
        

    }
}
