//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
//{
//    public class UserSecurityQueRepository : GenericRepository<UserSecurityQue, EnterprisegatewayportalDbContext>,IUserSecurityQueRepository
//    {
//        public UserSecurityQueRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger) { }

//        public async Task<IEnumerable<UserSecurityQue>> GetAllUserSecQueAnsAsync(int userId)
//        {
//            try
//            {
//                return await Context.UserSecurityQues.AsNoTracking().Where(uu => uu.UserId == userId).ToListAsync();
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }
//    }
//}
