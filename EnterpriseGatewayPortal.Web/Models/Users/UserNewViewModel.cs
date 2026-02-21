using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.Models.Users
{
    public class UserNewViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Email ")]
        public string MailId { get; set; }

        [Display(Name = "Gender")]
        public int gender { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Mobile number length should be 9 digit required")]
        [MinLength(9)]
        [MaxLength(9)]

        [Display(Name = "Mobile Number ")]
        public string MobileNo { get; set; }

        [Display(Name = "Role ")]
        //[Required]
        public int? RoleId { get; set; }

        public List<SelectListItem>? Roles { get; set; }
    }
}
