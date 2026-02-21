using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.KycApplications
{
    public class KycApplicationNewViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Application Name ")]
        [MaxLength(50)]
        public string ApplicationName { get; set; }

        [Display(Name = "Organisation Name")]
        public string OrganizationId { get; set; }

        [Display(Name = "Organization Name ")]
        public string OrgName { get; set; }

        [Required]
        [Display(Name = "Client Id")]
        public string ClientId { get; set; }

        [Required]
        [Display(Name = "Client Secret")]
        public string ClientSecret { get; set; }

    }
}
