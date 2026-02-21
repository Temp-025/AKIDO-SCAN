using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ISubscriberOrgTemplateService
    {
        Task<ServiceResult> AddSubscriberOrgTemplateAsync(Subscriberorgtemplate model);
        Task<ServiceResult> DeleteSubscriberOrgTemplateAsync(string tempID, UserDTO user);
        Task<ServiceResult> GetSubscriberOrgTemplateAsync(UserDTO userDTO);
        Task<ServiceResult> GetTemplateListByOrgId(string orgID);
    }
}
