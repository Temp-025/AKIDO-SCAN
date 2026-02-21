using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IOrganizationDetailService
    {
        Task<ServiceResult> AddOrganizationDetailAsync(OrganizationDetail model);
        Task<ServiceResult> DeleteOrganizationDetailAsync(OrganizationDetail model);
        Task<ServiceResult> DeleteOrganizationDetailByIdAsync(int id);
        Task<ServiceResult> GetAllOrganizationDetailListAsync();
        Task<ServiceResult> GetOrganizationDetailByIdAsync(int id);
        Task<ServiceResult> GetOrganizationDetailByUIdAsync(string uid);
        Task<ServiceResult> UpdateAgentUrlAndSpocEmailAsync(AgentUrlAndSpocUpdateDTO model);
        Task<ServiceResult> UpdateESealImageAsync(ESealImageUpdateDTO model);
        Task<ServiceResult> UpdateOrganizationDetailAsync(OrganizationDetail model);
    }
}
