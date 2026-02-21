using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface ISubscriptionVerifyService
    {

        //Task<IEnumerable<SubscriptionVerifyListDTO>> GetAllSubscriptionListByIdAsync(string orgId);
        Task<ServiceResult> GetAllSubscriptionListByIdAsync(string orgId);
        Task<ServiceResult> GetAllSubscriptionListAsync();
        Task<ServiceResult> SaveSubscriptionDetails(SubscriptionModelDTO subscriptionModelDTO);
        Task<ServiceResult> GetSubscriptionDetailByIdAsync(int id);
        Task<ServiceResult> GetAllSubscriptionByissuerIdAsync(string orgId);
        Task<ServiceResult> UpdateSubscriptionDetails(SubscriptionUpdateDTO subscriptionUpdateDTO);
    }
}
