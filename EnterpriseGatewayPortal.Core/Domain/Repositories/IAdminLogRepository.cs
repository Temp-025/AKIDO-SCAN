using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
	public interface IAdminLogRepository : IGenericRepository<AdminLog>
    {
		
		Task<AdminLog?> AddAdminLogAsync(AdminLog adminLog);

		Task<IEnumerable<AdminLog>> GetLatestLogsAsync(int count);

        Task<List<AdminLog>> GetLocalAdminLogReportByTimeStampAsync(string startDate, string endDate, string userName, string moduleName, int page, int perPage);
        Task<int> GetTotalLogCountAsync(string startDate, string endDate, string userName, string moduleName);

        Task<string[]> GetAllAdminLogUserNames(string request);
    }
}
