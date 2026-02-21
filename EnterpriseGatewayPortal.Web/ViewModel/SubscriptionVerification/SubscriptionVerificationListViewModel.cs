using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.SubscriptionVerification
{
    public class SubscriptionVerificationListViewModel
    {
        public IEnumerable<SubscriptionVerifyListDTO> SubscriptionVerifyLists { get; set; } = new List<SubscriptionVerifyListDTO>();

    }
}
