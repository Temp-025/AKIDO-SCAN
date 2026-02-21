using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IPaymentService
    {
        Task<ServiceResult> GetCreditDeatails(UserDTO userdata);
        Task<ServiceResult> IsCreditAvailable(UserDTO userdata, bool isEsealPresent, bool isSignaturePresent = false);
    }
}
