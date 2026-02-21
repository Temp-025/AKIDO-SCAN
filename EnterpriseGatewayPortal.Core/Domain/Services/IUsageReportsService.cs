using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IUsageReportsService
    {
        Task<IEnumerable<OrganizationUsageReportDTO>> GetOrganizationUsageReports(string organizationUid, string year);

        Task<string> DownloadUsageReport(int reportId);
        Task<ServiceResult> DownloadCurrentMonthUsageReport(string organizationUid);
    }
}
