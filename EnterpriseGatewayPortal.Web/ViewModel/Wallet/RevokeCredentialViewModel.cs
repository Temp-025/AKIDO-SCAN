using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.Wallet
{
    public class RevokeCredentialViewModel
    {
        [Required(ErrorMessage = "Credential is required")]
        [Display(Name = "User Credential")]
        public string? Credential { get; set; }
        public List<SelectListItem> CredentialList { get; set; }

        [Required(ErrorMessage = "Document ID is required")]
        [Display(Name = "User Document ID")]
        public string? DocumentID { get; set; }
    }
}
