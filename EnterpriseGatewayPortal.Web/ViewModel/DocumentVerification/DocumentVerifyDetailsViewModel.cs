using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.DocumentVerification
{
    public class DocumentVerifyDetailsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Issuer Organization Name")]
        public string? IssuerOrgName { get; set; }

        [Display(Name = "Verified By")]
        public string? VerifiedBy { get; set; }

        [Display(Name = "Requested By")]
        public string? RequestedBy { get; set; }
        [Display(Name = "Created At")]
        public string? CreatedAt { get; set; }
        [Display(Name = "Updated At")]
        public string? UpdatedAt { get; set; }
        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }
        [Display(Name = "Updated By")]
        public string? UpdatedBy { get; set; }

        [Display(Name = "Verification Type")]
        public string? VerificationType { get; set; }
        [Display(Name = "Document Type")]
        public string? DocumentType { get; set; }
        [Display(Name = "Completed At")]
        public string? CompletedAt { get; set; }
        [Display(Name = "Status")]
        public string? Status { get; set; }
        public string? FileId { get; set; }
    }
}
