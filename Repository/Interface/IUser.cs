
using CRUD_Using_Repository.Models;

namespace CRUD_Using_Repository.Repository.Interface
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetUsers();
        //void AddUser(User user);
        Task<int> AddUser(User user);

        Task<User> GetUserById(int id);
        Task<bool> UpdateRecord(User user);

        Task<bool> DeleteRecord(int id);

        Task<int> UpdateAuditLogs(AuditLogs auditlogs);

        Task<IEnumerable<AuditLogs>> Audits();

       
        

    }
}
