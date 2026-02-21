using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ITemplateService
    {
        Task<IList<SignatureTemplatesDTO>> GetSignatureTemplateListAsync();
        Task<OrganizationTemplatesDTO> GetOrganizationTemplates(string organizationUid);
        Task<APIResponse> UpdateOrganizationTemplates(OrganizationTemplatesDTO model);
        Task<ServiceResult> IsValid(UpdateTemplateDTO model,string orgUID);
        Task<ServiceResult> CheckOrgUserWithSignatureTemplate(GetTemplateDTO orgUser);

        Task<ServiceResult> GetSignaturePreviewAsync(UserDTO user);
    }
}
