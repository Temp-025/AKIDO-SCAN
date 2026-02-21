using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.Wallet
{
    public class CredentialAddViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Credential Name is required")]
        [Display(Name = "Credential Name")]
        public string? CredentialName { get; set; }
        public string? CredentialId { get; set; }

        [Required(ErrorMessage = "Verification DocType is required")]
        [Display(Name = "ID Document")]
        public string? VerificationDocType { get; set; }



        [Required(ErrorMessage = "Data Attributes is required")]
        [Display(Name = "Data Attributes")]
        public List<DataAttribute> DataAttributes { get; set; }

        [Required(ErrorMessage = "Authentication Scheme is required")]
        [Display(Name = "Authentication Scheme")]
        public string? AuthenticationScheme { get; set; }

        public List<SelectListItem> AuthSchemeTypesList { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]

        public string? Category { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
        public string? OrganizationId { get; set; }
        public string? ServiceDetails { get; set; }

        public string? Status { get; set; }

        [Required(ErrorMessage = "Logo is required")]
        [Display(Name = "Logo")]
        public string? Logo { get; set; }

        [Required(ErrorMessage = "Display Name is required")]
        [Display(Name = "Display Name")]
        public string? DisplayName { get; set; }

        [Required(ErrorMessage = "No. of days is required")]
        [Display(Name = "Days")]
        public string? validity { get; set; }
        [Required(ErrorMessage = "Trust Gateway Credential Url is required")]
        [Display(Name = "Trust Gateway Credential Url")]
        public string? CredentialTrustUrl { get; set; }

        public List<DataAttribute> DefaultDataAttributes { get; set; }
        public List<OrganizationCategoryDTO> OrganizationCategories { get; set; }
        public List<int> SelectedOrgCategoryIds { get; set; } = new List<int>();


    }
}
