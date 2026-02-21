using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IOrgSignatureTemplateRepository : IGenericRepository<OrgSignatureTemplate>
    {
        OrgSignatureTemplate AddOrgSignatureTemplate(OrgSignatureTemplate model);
        Task<OrgSignatureTemplate?> AddOrgSignatureTemplateAsync(OrgSignatureTemplate model);
        Task<IEnumerable<OrgSignatureTemplate>> GetAllOrgSignatureTemplateAsync();
        Task<IEnumerable<OrgSignatureTemplate>> GetAllOrgSignatureTemplateByUIDAsync(string uid);
        Task<OrgSignatureTemplate> GetOrgSignatureTemplateByIdAsync(int id);
        Task<OrgSignatureTemplate> GetOrgSignatureTemplateByUIDAsync(string uid);
        Task<bool> IsOrgSignatureTemplateExistsWithUIDAsync(string uid);
        bool RemoveOrgSignatureTemplate(OrgSignatureTemplate model);
        Task<bool> RemoveOrgSignatureTemplateById(int id);
        Task<OrgSignatureTemplate> UpdateOrgSignatureTemplate(OrgSignatureTemplate model);
    }
}
