using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.BuyCredits
{
    public class PaymentRequestViewModel
    {
        public string MobNumber { get; set; }
        public PaymentRequestDTO PaymentRequests {get; set; }
    }
}
