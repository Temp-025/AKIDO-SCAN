using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.ESealRegistration
{
    public class UpdateSpocAndAgentUrlViewModel
    {
        public string OrganizationUID { get; set; }
        public string? AgentUrl { get; set; }

        [Display(Name = "SPOC UAEID Email")]
        [Required(ErrorMessage = "Invalid email address")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public string? SpocUgpassEmail { get; set; }
    }
}
