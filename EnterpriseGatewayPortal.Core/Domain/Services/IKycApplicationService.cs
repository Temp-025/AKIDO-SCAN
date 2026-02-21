using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IKycApplicationService
    {
        Task<ServiceResult> GetKycApplicationsListByOrgId(string orgUid);
        Task<ServiceResult> GetKycApplicationById(int id);
        Task<ServiceResult> SaveKycApplication(KycApplicationDTO dto);
        Task<ServiceResult> UpdateKycApplication(KycApplicationDTO dto);
    }
}
