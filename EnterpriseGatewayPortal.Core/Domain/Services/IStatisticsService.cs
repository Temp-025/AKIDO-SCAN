using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IStatisticsService
    {
        Task<GraphDTO> GetGraphCountAsync(string serviceProviderName);
        Task<IEnumerable<AdminActivity>> GetAdminLogReportAsync(int page = 1);
        Task<GraphDTO> GetOranizationGraphCountAsync();
    }
}
