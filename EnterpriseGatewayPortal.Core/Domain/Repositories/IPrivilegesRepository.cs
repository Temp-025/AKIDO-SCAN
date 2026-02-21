using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IPrivilegesRepository
    {
        Task<List<Privilege>> GetAllActivePrivilegesAsync();
    }
}
