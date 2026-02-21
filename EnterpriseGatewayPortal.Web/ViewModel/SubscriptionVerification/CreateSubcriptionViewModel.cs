using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.SubscriptionVerification
{
    public class CreateSubcriptionViewModel
    {
        public List<IssuerOrgNamesDTO> IssuerOrgNameDetails { get; set; }
        public string issuerName { get; set; }
        public string issuerUid { get; set; }
        public string selectedDocuments { get; set; }
    }
}
