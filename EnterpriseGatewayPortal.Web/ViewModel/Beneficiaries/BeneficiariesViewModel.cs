using DocumentFormat.OpenXml.Wordprocessing;
using EnterpriseGatewayPortal.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.Beneficiaries
{
    public class BeneficiariesViewModel
    {
        public int orgContactsEmailId { get; set; }

        //[Display(Name = "Beneficiary Office Email")]
        //public string EmployeeEmail { get; set; }

        public string? OrganizationUid { get; set; }


        [Display(Name = "Designation")]
        public string? Designation { get; set; }

        [Display(Name = "Signature Photo(Accepted max 20kb)")]
        public string? SignaturePhoto { get; set; }

        [Required]
        [Display(Name = "Beneficiary Email")]
        public string? UgpassEmail { get; set; }

        [Display(Name = "Passport Number")]
        public string? PassportNumber { get; set; }

        [Display(Name = "National Id Number")]
        public string? NationalIdNumber { get; set; }

        [Display(Name = "Mobile Number")]
        public string? MobileNumber { get; set; }

        [Display(Name = "Sponsor Type")]
        public string? SponsorType { get; set; }

        [Display(Name = "Beneficiary Type")]
        public string? BeneficiaryType { get; set; }

        public string? SubscriberUid { get; set; }

        public string? Status { get; set; }

        public List<BeneficiariesServicesDTO> ServicePrivilages { get; set; }

        //public List<ValidityDTO> BeneficiaryValidities { get; set; }
    }
}
