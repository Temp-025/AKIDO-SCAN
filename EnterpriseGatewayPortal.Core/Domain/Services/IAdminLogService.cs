using EnterpriseGatewayPortal.Core.Domain.Models;
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
	public interface IAdminLogService
	{
		Task SaveAdminLogDetails(AdminLogDTO adminLogDTO);
		Task<IEnumerable<AdminLog>> GetLatestLogsAsync(int count);

        Task<PaginatedList<AdminLogReportDTO>> GetLocalAdminLogReportsAsync(string startDate, string endDate, string userName = null,string moduleName = null, int page = 1, int perPage = 10);

        Task<string[]> GetAllAdminLogNames(string request);
    }
}
