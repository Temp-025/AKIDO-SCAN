using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface ISubscriberOrgTemplateRepository : IGenericRepository<Subscriberorgtemplate>
    {
        Task<bool> DeleteSubscriberOrgTemplate(string templateId, UserDTO userDTO);
        Task<IList<Subscriberorgtemplate>> GetTemplateListByOrgId(string orgId);
        Task<IList<Subscriberorgtemplate>> GetTemplateListBySuidAndOrgId(string suid, string orgId);
        Task<Subscriberorgtemplate> SaveSubscriberOrgTemplate(Subscriberorgtemplate subscriberOrgTemplate);
    }
}
