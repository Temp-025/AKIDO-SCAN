using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IConfigurationService
    {
        Task<ServiceResult> AddConfigurationAsync(Configuration model);
        Task<ServiceResult> GetAllConfiguration();
    }
}
