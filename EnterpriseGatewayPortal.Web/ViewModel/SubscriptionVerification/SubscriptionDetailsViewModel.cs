using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.SubscriptionVerification
{
    public class SubscriptionDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Issuer Organization Name")]
        public string? DocumentIssuerName { get; set; }

        [Display(Name = "Document Type")]
        public string? DocumentTypes { get; set; }

        [Display(Name = "Expiry Date")]
        public string? ExpiryDate { get; set; }
        [Display(Name = "Subscription Date")]
        public string? SubscriptionDate { get; set; }

        [Display(Name = "Status")]
        public string? Status { get; set; }

        [Display(Name = "Subscription Fee")]
        public string? SubscriptionFee { get; set; }

        [Display(Name = "Created At")]
        public string? CreatedAt { get; set; }

        [Display(Name = "Updated At")]
        public string? UpdatedAt { get; set; }

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Updated By")]
        public string? UpdatedBy { get; set; }
    }
}
