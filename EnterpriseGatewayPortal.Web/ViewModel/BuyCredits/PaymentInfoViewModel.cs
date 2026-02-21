namespace EnterpriseGatewayPortal.Web.ViewModel.BuyCredits
{
    public class PaymentInfoViewModel
    {
        public List<ServiceInfo> PaymentInfo { get; set; }
        public double GrandTotal { get; set; }
        public string PayerMobile { get; set; }
        public string OrgId { get; set; }
    }

    public class ServiceInfo
    {
        public double Discount { get; set; }
        public double Rate { get; set; }
        public string PrevilageServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceId { get; set; }
        public double Tax { get; set; }
        public double Volume { get; set; }
    }

}
