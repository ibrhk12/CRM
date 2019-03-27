using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM.DataAccess.Interfaces
{
    public interface IUsersManager
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetUser(Object id);
        Task<Users> GetUser(string userName);
        Task AddUser(Users item);
        Task<bool> RemoveUser(string userName);
        Task<bool> UpdateUser(string userName, Users item);
        Task<bool> RemoveAllUsers();

    }
}
