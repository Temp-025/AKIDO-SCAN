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
    public interface IAdminLogReportsService
    {
        Task<PaginatedList<AdminLogReportDTO>> GetAdminLogReportAsync(string startDate, string endDate, string userName = null,
          string moduleName = null, int page = 1, int perPage = 10);

        ServiceResult VerifyChecksum(object logReport);
    }
}
