using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EnterpriseGatewayPortal.Web.Models.SecurityQuestions
{
    public class ProfileViewModel
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Gender")]
        public int gender { get; set; }

        [Required]
        //[EmailAddress]
        [MaxLength(50)]
        [Display(Name = "Email ")]
        public string MailId { get; set; }

       
        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid 10-digit mobile number.")]
       
        [Display(Name = "Mobile Number ")]
        public string MobileNo { get; set; }


        public int RoleId { get; set; }
        public string Role { get; set; }

        [Display(Name = "Status ")]
        public string Status { get; set; }

        
    }
}
