using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        void AddUser(User model);
        Task AddUserAsync(User model);
        Task<IEnumerable<User>> GetAllUserAsync();
        User GetUserById(int id);
        Task<User> GetUserByIdAsync(int id);
        void ReloadUser(User model);
        void RemoveUser(User model);
        void UpdateUser(User model);
        Task<bool> IsUserExistsbyPhonenoAsync(string MobileNumber);
        Task<bool> IsUserExistsbyEmailAsync(string Email);
        Task<User> IsUserExist(string Email);
        Task<List<User>> GetAllUsersWithRolesAsync();
        Task<User> GetUserbyPhoneno(string Phoneno);
        Task<User> GetUserbyUuidAsync(string uuid);
    }
}
