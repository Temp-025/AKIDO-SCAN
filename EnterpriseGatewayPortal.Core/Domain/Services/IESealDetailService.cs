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
    public interface IESealDetailService
    {
        Task<ServiceResult> GetEsealCertificateStatus(string organizationUid);
        Task<EsealImageDTO> GetESealLogoAsync(string organizationUid);
        Task<ServiceResult> UpdateEsealLogo(string base64Image, string organizationUid);
        Task<OrganizationCertificate> GetCertificateDetails(string organizationUid);
    }
}
