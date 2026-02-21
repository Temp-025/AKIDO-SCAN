using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User, EnterprisegatewayportalDbContext>, IUserRepository
    {
        public UserRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger) { }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                return await GetByIdAsync(id);
            }
            catch (Exception error)
            {
                Logger.LogError("GetUserByIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<User> GetUserbyUuidAsync(string uuid)
        {
            try
            {
                return await Context.Users.AsNoTracking().SingleOrDefaultAsync(uu => uu.Uuid == uuid); ;
            }
            catch (Exception error)
            {
                Logger.LogError("GetUserByIdAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                return GetById(id);
            }
            catch (Exception error)
            {
                Logger.LogError("GetUserById:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            try
            {
                return await GetAllAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllUserAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task AddUserAsync(User model)
        {
            try
            {
                await AddAsync(model);
            }
            catch (Exception error)
            {
                Logger.LogError("AddUserAsync:: Database Exception: {0}", error);
            }
        }

        public void AddUser(User model)
        {
            try
            {
                Add(model);
            }
            catch (Exception error)
            {
                Logger.LogError("AddUser:: Database Exception: {0}", error);
            }
        }

        public void UpdateUser(User model)
        {
            try
            {
                Update(model);
                //Context.Attach(model).State = EntityState.Modified;
            }
            catch (Exception error)
            {
                Logger.LogError("UpdateUser:: Database Exception: {0}", error);
            }
        }

        public void ReloadUser(User model)
        {
            try
            {
                Reload(model);
            }
            catch (Exception error)
            {
                Logger.LogError("ReloadUser:: Database Exception: {0}", error);
            }
        }
        public void RemoveUser(User model)
        {
            try
            {
                Remove(model);
            }
            catch (Exception error)
            {
                Logger.LogError("RemoveUser:: Database Exception: {0}", error);
            }
        }
        public async Task<User> IsUserExist(string Email)
        {
            try
            {
                return await Context.Users.AsNoTracking().SingleOrDefaultAsync(uu => uu.Email == Email && uu.Status!= "DELETED");
            }
            catch (Exception error)
            {
                Logger.LogError("GetUserbyEmailAsync::Database exception: {0}", error);
                return null;
            }
        }
        public async Task<User> GetUserbyPhoneno(string Phoneno)
        {
            try
            {
                return await Context.Users.AsNoTracking().SingleOrDefaultAsync(uu => uu.MobileNumber == Phoneno);
            }
            catch (Exception error)
            {
                Logger.LogError("GetUserbyEmailAsync::Database exception: {0}", error);
                return null;
            }
        }
        public async Task<bool> IsUserExistsbyEmailAsync(string Email)
        {
            try
            {
                return await Context.Users
                    .AsNoTracking().AnyAsync(u => u.Email == Email);
            }
            catch (Exception error)
            {
                Logger.LogError("IsUserExistsbyUuidAsync::Database exception: {0}", error);
                return false;
            }
        }

        public async Task<bool> IsUserExistsbyPhonenoAsync(string MobileNumber)
        {
            try
            {
                return await Context.Users
                    .AsNoTracking().AnyAsync(u => u.MobileNumber == MobileNumber);
            }
            catch (Exception error)
            {
                Logger.LogError("IsUserExistsbyUuidAsync::Database exception: {0}", error);
                return false;
            }
        }
        public async Task<List<User>> GetAllUsersWithRolesAsync()
        {
            try
            {
                var list = await Context.Users.AsNoTracking()
                    .Where(x => x.Status != "DELETED")
                    .Include(u => u.Role).ToListAsync();

                return list;
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllUsersWithRolesAsync::Database exception: {0}", error);
                return null;
            }
        }
    }
}
