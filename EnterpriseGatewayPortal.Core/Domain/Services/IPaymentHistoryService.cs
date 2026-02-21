using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IPaymentHistoryService
    {
        Task<IEnumerable<OrganizationPaymentHistoryDTO>> GetOrganizationPaymentHistoryAsync(string orgUid);
        Task<IEnumerable<ServiceDefinitionDTO>> GetServiceDefinitionsAsync();
        Task<ServiceResult> GetPaymentInfoByTransactionRefId(string Id);
    }
}
