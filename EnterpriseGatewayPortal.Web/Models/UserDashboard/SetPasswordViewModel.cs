using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EnterpriseGatewayPortal.Web.Models.SecurityQuestions
{
    public class SetPasswordViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
