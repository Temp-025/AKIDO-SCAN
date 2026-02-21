using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ILocalBusinessUsersService
    {
        Task<ServiceResult> AddBusinessUserAsync(OrgSubscriberEmail model);
        Task<ServiceResult> AddBusinessUsersListAsync(List<OrgSubscriberEmail> models);
        Task<ServiceResult> GetAllBusinessUserListAsync();
        Task<ServiceResult> GetAllBusinessUsersByOrgUidAsync(string orgUid);
        Task<ServiceResult> GetBusinessUserByIdAsync(int id);
        Task<ServiceResult> GetBusinessUserByOrgUidAsync(string orgUid);
        Task<ServiceResult> UpdateBusinessUserAsync(OrgSubscriberEmail model);
        Task<ServiceResult> DeleteBusinessUserAsync(OrgSubscriberEmail model);
        Task<ServiceResult> GetBusinessUserByEmailAsync(string Email);
        Task<ServiceResult> GetBusinessUserByEmployeeEmailAsync(string Email);
        Task<ServiceResult> GetBulkSignerListAsync();
    }
}
