using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.Models.Template
{
    public class UpdateTemplatesViewModel
    {
        public IEnumerable<SignatureTemplate> TemplateList { get; set; }
        [Display(Name = "Select signature template")]
        [Required(ErrorMessage = "Select signature template")]
        public int SignatureTemplate { get; set; }

        [Display(Name = "Select ESeal Template")]
        //[Required(ErrorMessage = "Select ESeal Template")]
        public int ESealTemplate { get; set; }
    }
}
