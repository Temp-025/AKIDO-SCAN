using DocumentFormat.OpenXml.Wordprocessing;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Web.ViewModel.BusinessUser;
using EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.DocumentIssuer
{
    public class CreateDocIssuerViewModel
    {
        public int id { get; set; }

        [Display(Name = "Document Name")]
        public string documentName { get; set; }

        [Display(Name = "Techincal")]
        public bool techincal { get; set; }

        [Display(Name = "QR Code")]
        public bool qrcode { get; set; }

        [Display(Name = "True Copy Verification")]
        public bool trueCopyVerification { get; set; }

        [Display(Name = "Relaying Party")]
        public bool relayingParty { get; set; }

        [Display(Name = "Professional")]
        public bool professional { get; set; }

        [Display(Name = "Individual")]
        public bool individual { get; set; }

        [Display(Name = "Techinal verification")]
        public string? daysForTechincal { get; set; }

        [Display(Name = " QRCode verification")]
        public string? daysForQrcode { get; set; }

        [Display(Name = " True Copy verification")]
        public string? daysForTrueCopyVerification { get; set; }

        [Display(Name = "Techinal verification")]
        public string? priceForTechincal { get; set; }

        [Display(Name = " QRCode verification")]
        public string? priceForQrcode { get; set; }

        [Display(Name = "True Copy verification")]
        public string? priceForTrueCopyVerification { get; set; }

        [Display(Name = " Subscription Fee")]
        public decimal subscriptionFee { get; set; }

        public IEnumerable<OrgSubscriberEmail> businessUsers { get; set; }

        public string? allowedConsumers { get; set; }

        public string? allowedIssuers { get; set; }


    }
}
