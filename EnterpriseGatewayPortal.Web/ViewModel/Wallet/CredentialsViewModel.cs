using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.Wallet
{
    public class CredentialsViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Credential Name")]
        public string? CredentialName { get; set; }
        public string? CredentialId { get; set; }

        [Display(Name = "ID Document")]
        public string? VerificationDocType { get; set; }

        [Display(Name = "Data Attributes")]
        public List<DataAttribute> DataAttributes { get; set; }

        [Display(Name = "Issuer Authentication Scheme")]
        public string? AuthenticationScheme { get; set; }
        public string? CategoryId { get; set; }

        [Display(Name = "Category")]
        public string? Category { get; set; }
        public string? OrganizationId { get; set; }
        public string? ServiceDetails { get; set; }
        public string? Status { get; set; }
        public string? CredentialUId { get; set; }
        [Display(Name = "Trust Gateway Credential Url")]
        public string? CredentialTrustUrl { get; set; }
        public string? DisplayName { get; set; }
        public string? Logo { get; set; }
        public string? Config { get; set; }
        public IFormFile File { get; set; }
        public bool qrCodeRequired { get; set; }
        public string? DocumentName { get; set; }
        public int Credential_localId { get; set; }
        public List<SelectListItem> AuthSchemeTypesList { get; set; }
        public List<SelectListItem> CategoryList { get; set; }

        public string validity { get; set; }
        public List<OrganizationCategoryDTO> OrganizationCategories { get; set; }
        public List<int> SelectedOrgCategoryIds { get; set; } = new List<int>();


    }
}
