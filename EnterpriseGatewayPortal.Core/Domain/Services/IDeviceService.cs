using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IDeviceService
    {
        Task<ServiceResult> checkStatus(string deviceId, string clientId);
        Task<ServiceResult> CheckLicence(string clientId, string type);
        Task<ServiceResult> ActiveLicence(string clientId, string type);
        Task<ServiceResult> ReadLicence();
    }
}
