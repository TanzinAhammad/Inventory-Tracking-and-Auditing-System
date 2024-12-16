using CRUD_Using_Repository.Data;
using CRUD_Using_Repository.Migrations;
using CRUD_Using_Repository.Models;
using CRUD_Using_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Using_Repository.Repository.Service
{
    public class UserService : IUser
    {
        private readonly ApplicationContext context;
        public UserService(ApplicationContext context) 
        {
            this.context = context;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            var data = await context.Users.ToListAsync();
            return data;
        }
        public async Task<int> AddUser(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user.SKU;
        }

        Task<User> IUser.GetUserById(int id)
        {
            var data = context.Users.Where(e => e.SKU == id).FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> UpdateRecord(User user)
        {
            bool status = false;
            if (user != null)
            {
                context.Users.Update(user);
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
                var data = await context.Users.Where(e => e.SKU == id).FirstOrDefaultAsync();
                if(data != null)
                {
                    context.Users.Remove(data);
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

        //public async Task<user> GetUserById(int id)
        //{

        //}
    }
}
