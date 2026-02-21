using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.SignatureDelegation
{
    public class ViewDelegateViewModel
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string DelegationStatus { get; set; }
        public string DelegationID { get; set; }
        public List<DelegateRecep> Delegatees { get; set; }
    }
    
}
