using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.Beneficiaries
{
    public class BeneficiariesEditViewModel
    {

        public int? Id { get; set; }
        public string? SponsorDigitalId { get; set; }
        public string? SponsorName { get; set; }
        public string? SponsorType { get; set; }
        public string? SponsorExternalId { get; set; }
        public string? BeneficiaryName { get; set; }
        public string? BeneficiaryDigitalId { get; set; }
        public string? BeneficiaryType { get; set; }
        public int? SponsorPaymentPriorityLevel { get; set; }
        public string? BeneficiaryNin { get; set; }
        public string? BeneficiaryPassport { get; set; }
        public string? BeneficiaryMobileNumber { get; set; }
        // public string BeneficiaryOfficeEmail { get; set; }
        [Required]
        public string? BeneficiaryUgpassEmail { get; set; }
        public bool BeneficiaryConsentAcquired { get; set; }
        public string? SignaturePhoto { get; set; }
        public string? Designation { get; set; }
        public string? Status { get; set; }
        public string? countryCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
       public List<BeneficiariesValidityDTO> BeneficiaryValidities { get; set; }
       //public List<BeneficiaryValidity> BeneficiaryValidities { get; set; }
        public List<BeneficiariesServicesDTO> BeneficiariedPrivilegeList { get; set; }
    }

    //public class BeneficiariesValidityDTO
    //{
    //    public int? Id { get; set; }
    //    public int? BeneficiaryId { get; set; }
    //    public int? PrivilegeServiceId { get; set; }
    //    public bool ValidityApplicable { get; set; }
    //    public DateTime? Valid_From { get; set; }
    //    public DateTime? ValidUpto { get; set; }
    //    public string? Status { get; set; }
    //    public DateTime? CreatedOn { get; set; }
    //    public DateTime? UpdatedOn { get; set; }
    //}

    //public class BeneficiariedPrivilegeDTO
    //{
    //    public int? PrivilegeId { get; set; }
    //    public string? PrivilegeServiceName { get; set; }
    //    public string? PrivilegeServiceDisplayName { get; set; }
    //    public string? Status { get; set; }
    //    public int? IsChargable { get; set; }
    //}


}
