//using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.BusinessUser
{
    public class BusinessUserEditViewModel
    {
        public int orgContactsEmailId { get; set; }

        [Display(Name = "Employee Email")]
        public string EmployeeEmail { get; set; }

        public bool Signatory { get; set; } = true;

        [Display(Name = "Affix Eseal")]
        public bool ESealSignatory { get; set; }

        [Display(Name = "Preparatory(eseal and Template)")]
        public bool ESealPrepatory { get; set; }

        public bool Template { get; set; }

        [Display(Name = "Bulk Signer")]
        public bool Bulksign { get; set; }

        public string OrganizationUid { get; set; }

        [Display(Name = "Delegation")]
        public bool Delegate { get; set; }

        [Display(Name = "Digital Form Preparation")]
        public bool DigitalForm { get; set; }

        [Display(Name = "Local Signing")]
        public bool LsaPrivilege { get; set; }

        [Display(Name = "Designation")]
        public string? Designation { get; set; }

        [Display(Name = "Signature Photo(Accepted max 20kb)")]
        public string? SignaturePhoto { get; set; }

        [Display(Name = "Initial Image(Accepted max 20kb)")]
        public string? InitialImage { get; set; }

        [Display(Name = "UAEID Email")]
        public string? UgpassEmail { get; set; }

        [Display(Name = "Passport Number")]
        public string? PassportNumber { get; set; }

        [Display(Name = "National Id Number")]
        public string? NationalIdNumber { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Mobile number length should be 10 digit required")]
        
        public string? MobileNumber { get; set; }

        public string? SubscriberUid { get; set; }

        public string Status { get; set; }

        public bool UgpassUserLinkApproved { get; set; } = false;

        public bool IsSignatureUploaded { get; set; }

        public string ExistingSignaturePhoto { get; set; }

        public string ExistingInitialImage { get; set; }
        public int SignatureTemplate { get; set; }
    }
}
