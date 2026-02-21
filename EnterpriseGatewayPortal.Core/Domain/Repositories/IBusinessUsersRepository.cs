using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IBusinessUsersRepository
    {
        OrgSubscriberEmail AddOrgSubscriberEmail(OrgSubscriberEmail model);
        Task<OrgSubscriberEmail?> AddOrgSubscriberEmailAsync(OrgSubscriberEmail model);
        Task<List<OrgSubscriberEmail>> AddOrgSubscriberEmailsListAsync(List<OrgSubscriberEmail> models);
        Task<IEnumerable<OrgSubscriberEmail>> GetAllOrgSubscriberEmailAsync();
        Task<IEnumerable<OrgSubscriberEmail>> GetAllOrgSubscriberEmailsByOrgUIDAsync(string uid);
        Task<OrgSubscriberEmail> GetOrgSubscriberEmailByIdAsync(int id);
        Task<OrgSubscriberEmail> GetOrgSubscriberEmailByUIDAsync(string uid);
        Task<bool> IsOrgSubscriberEmailExistsWithUIDAsync(string uid);
        bool RemoveOrgSubscriberEmail(OrgSubscriberEmail model);
        Task<bool> RemoveOrgSubscriberEmailById(int id);
        Task<OrgSubscriberEmail> UpdateOrgSubscriberEmail(OrgSubscriberEmail model);
        Task<OrgSubscriberEmail> GetOrgSubscriberEmailByEmailAsync(string Email);
        Task<OrgSubscriberEmail> GetOrgSubscriberEmailByEmployeeEmailAsync(string Email);
        Task<IEnumerable<OrgSubscriberEmail>> GetAllOrgSubscriberEmailwithBulkSignAsync();
    }
}
