using CRUD_Using_Repository.Data;
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
            return user.UserId;
        }
    }
}
