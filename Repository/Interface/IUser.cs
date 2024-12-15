﻿
using CRUD_Using_Repository.Models;

namespace CRUD_Using_Repository.Repository.Interface
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetUsers();
        //void AddUser(User user);
        Task<int> AddUser(User user);
    }
}