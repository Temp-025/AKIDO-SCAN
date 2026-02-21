using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.BuyCredits
{
    public class AvailableCreditsViewModel
    {
        public bool PostPaid { get; set; }
        public double Eseal_SIGNATURE { get; set; }
        public double Digital_SIGNATURE { get; set; }
        public double User_SUBSCRIPTION { get; set; }
        public string? orgId { get; set; }
        public string? serviceId { get; set; }
        public string? Credits {  get; set; }
        public List<SpocServices>? SpocServices { get; set; }
        public List<VolumeSpocServices>? volumesRanges { get; set; }

        /*public string? Digital_signature_credits { get; set; }
        public string? Digital_signature_Display { get; set; }
        public double Digital_signature_Discount { get; set; }
        public double Digital_signature_Tax { get; set; }

        public string? Eseal_signature_credits { get; set; }
        public string? Eseal_signature_Display { get; set; }
        public double Eseal_signature_Discount { get; set; }
        public double Eseal_signature_Tax { get; set; }

        public string? User_credits { get; set; }
        public string? User_Display { get; set; }

        public double User_Discount { get; set; }
        public double User_Tax { get; set; }*/



        public List<PriceSummary>? PriceSummaryList { get; set; }

        public CreditsSummeryViewModel CreditsModel { get; set; }

    }
    public class PriceSummary
    {
        public string? Credits { get; set; }
        public double Discount { get; set; } 
        public double Tax { get; set; } 
        public double Rate {  get; set; }
        public double ServiceId {  get; set; }
        public string PrevilageServiceID { get; set; }
        public string? DisplayName { get; set; }
        public string? OrgId { get; set; }
    }

    public class VolumeSpocServices
    {
        public string SpocserviceID { get; set; }
        public string volumeFrom { get; set; }
        public string volumeTo { get; set; }
        public string discountPercentage { get; set; }
    }

}
