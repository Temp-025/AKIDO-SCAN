using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.SubscriptionVerification
{
    public class EditSubcriptionViewModel
    {
        public List<IssuerOrgNamesDTO> IssuerOrgNameDetails { get; set; }
        public string DocumentNames { get; set; }
        public string IssuerOrgName { get; set; }
        public string issuerName { get; set; }
        public string issuerUid { get; set; }
        public string selectedDocuments { get; set; }
    }
}
