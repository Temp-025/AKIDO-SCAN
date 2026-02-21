using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IKYCLogReportsService
    {
        Task<IEnumerable<AdminActivity>> GetKYCLogReportAsync(int page = 1);
        Task<PaginatedList<LogReportDTO>> GetKYCLogReportAsync(string startDate, string endDate, string orgId, string serviceNamesCsv, int page = 1, int perPage = 10);
       // Task<IEnumerable<KycMethodDTO>> GetAllKYCMethodAsync();

        Task<LogReportDTO> GetKycDetailsAsync(string Id);
        Task<PaginatedList<KYCValidationResponseDTO>> GetKYCValidationReportAsync(
            string identifier = "",
            string logMessageType = "",
            string organizationId = "",
            string startDate = "",
            string endDate = "",
            List<string>? kycMethods = null,
            int page = 1,
            int perPage = 10
        );

        Task<OrgKycSummaryDTO> GetOrganizationIdValidationSummaryAsync(string orgId);

        Task<ServiceResult> GetKycDevicesOfOrg(string? orgId);

        Task<ServiceResult> GetKycDevicesCountOfOrg(string? orgId);

        Task<string[]> GetKycMethodsListAysnc(string orgUid);
        Task<ServiceResult> BlockKycDevice(KycDeviceBlockDTO dto);
        Task<ServiceResult> ValidateSignedDataAsync(string signedData, string serviceName);

    }
}
