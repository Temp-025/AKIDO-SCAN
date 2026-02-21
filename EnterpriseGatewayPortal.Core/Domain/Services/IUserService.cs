using EnterpriseGatewayPortal.Core.Domain.Lookups;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IUserService
    {
        Task<UserResponse> IsUserValid(string Email, string Password);
        Task<IEnumerable<User>> GetUsersList();
        Task<UserResponse> AddUser(User user);
        Task<UserResponse> UpdateUser(User user);
        Task<IEnumerable<RoleLookupItem>> GetRoleLookupsAsync();
        Task<UserResponse> EditUser(int Id);
        Task<User> GetUserAsync(int Id);
        Task<User> GetUserByEmail(string Email);
        Task<List<User>> ListUsersAsync();
        Task<UserResponse> AdminResetPassword(int id);
        Task<UserResponse> DeleteUser(int Id);
        Task<UserResponse> ActivateUser(int Id);
        Task<bool> IsUserExist(string Email);
    }
}
