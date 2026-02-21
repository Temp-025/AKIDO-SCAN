namespace EnterpriseGatewayPortal.Web.ViewModel.SignatureDelegation
{
    public class AddDelegationViewModel
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<string> Emails { get; set; }
    }
}
