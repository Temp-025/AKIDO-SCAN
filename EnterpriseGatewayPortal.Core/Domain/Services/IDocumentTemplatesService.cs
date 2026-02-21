using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IDocumentTemplatesService
    {
        Task<ServiceResult> GetBulkSignerListAsync(string OrgId, string token);

        Task<ServiceResult> SaveBulksignTemplateAsync(SaveNewTemplateDTO saveNewTemplateDTO, string token);

        Task<IEnumerable<DocumentsTemplatesListDTO>> GetTemplatesListAsync(string token);

        Task<ServiceResult> GetTemplateDetailsAsync(string templateId, string token);

        Task<ServiceResult> GetPreviewTemplateAsync(int edmsid, string token);
        Task<ServiceResult> GetTemplateListByOrgUid(string token);

        Task<ServiceResult> UpdateBulksignTemplateAsync(SaveNewTemplateDTO saveNewTemplateDTO, string token);

        Task<ServiceResult> CheckUserWithSignatureTemplate(DTVerifyOrganizationUserDTO dTVerifyOrganizationUserDTO, string token);
    }
}
