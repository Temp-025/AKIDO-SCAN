using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IUserSecurityQueRepository: IGenericRepository<UserSecurityQue>
    {
        Task<IEnumerable<UserSecurityQue>> GetAllUserSecQueAnsAsync(int userId);
    }
}
