using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IOrganizationCertificateService
    {
        Task<ServiceResult> AddOrganizationCertificateAsync(OrganizationCertificate model);
        Task<ServiceResult> DeleteOrganizationCertificateAsync(OrganizationCertificate model);
        Task<ServiceResult> DeleteOrganizationCertificateByIdAsync(int id);
        Task<ServiceResult> GetAllOrganizationCertificateListAsync();
        Task<ServiceResult> GetOrganizationCertificateByIdAsync(int id);
        Task<ServiceResult> GetOrganizationCertificateStatusByUIdAsync(string uid);
        Task<ServiceResult> UpdateOrganizationCertificateAsync(OrganizationCertificate model);
        Task<ServiceResult> GetOrganizationActiveCertificateByAsync();
    }
}
