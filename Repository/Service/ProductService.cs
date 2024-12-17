using Inventory.Data;
using Inventory.Models;
using Inventory.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repository.Service
{
    public class ProductService : IProduct
    {
        private readonly ApplicationContext context;
        public ProductService(ApplicationContext context) 
        {
            this.context = context;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var data = await context.Products.ToListAsync();
            return data;
        }
        public async Task<int> AddProduct(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return product.ProductId;
        }

        Task<Product> IProduct.GetProductById(int id)
        {
            var data = context.Products.Where(e => e.ProductId == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> UpdateRecord(Product product)
        {
            bool status = false;
            if (product != null)
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();
                status = true;
            }
             return status;
        }

        public async Task<bool> DeleteRecord(int id)
        {
            bool status = false;
            if(id!=0)
            {
                var data = await context.Products.Where(e => e.ProductId == id).FirstOrDefaultAsync();
                if(data != null)
                {
                    context.Products.Remove(data);
                    await context.SaveChangesAsync();
                    status = true;
                }
            }
            return status;
        }

        public async Task<int> UpdateAuditLogs(AuditLogs auditlogs)
        {
            await context.AuditLogs.AddAsync(auditlogs);
            await context.SaveChangesAsync();
            return auditlogs.AuditId;
        }

        public async Task<IEnumerable<AuditLogs>> Audits()
        {
            var auditData = await context.AuditLogs.ToListAsync();
            return auditData;
        }

        //public async Task<product> GetProductById(int id)
        //{

        //}
    }
}
