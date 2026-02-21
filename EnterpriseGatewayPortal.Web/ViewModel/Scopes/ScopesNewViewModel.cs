using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.Scopes
{
    public class ScopesNewViewModel
    {

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Require user consent for this scope")]
        public bool UserConsent { get; set; }

        [Required]
        [Display(Name = "Set as default scope")]
        public bool DefaultScope { get; set; }

        [Required]
        [Display(Name = "Include in public metadata")]
        public bool Metadata { get; set; }

        [Required]
        [Display(Name = "Required Claims")]
        public bool isClaimPresent { get; set; }

        public string claims { get; set; }
        public IEnumerable<ClaimListItem> ClaimLists { get; set; }
    }
}
