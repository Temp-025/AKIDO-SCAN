using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ILocalPrivilegesService
    {
        Task<ServiceResult> GetAllPrivilegesListAsync();
    }
}
