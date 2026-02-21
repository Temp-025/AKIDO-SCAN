using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IScopeService
    {
        Task<IEnumerable<UserClaimDTO>> GetScopeAttributeList();

        Task<ServiceResult> CreateScopeAsync(Scope scope);

        Task<Scope> GetScopeById(int id);

        Task<ServiceResult> UpdateScopeAsync(Scope scope);

        Task<ServiceResult> DeleteScopeById(int id);
    }
}
