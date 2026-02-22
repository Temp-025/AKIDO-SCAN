using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.Models.Users
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "UUID")]
        public string Uuid { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        [MinLength(4)]
        public string FullName { get; set; }

        [Display(Name = "Gender")]
        public int gender { get; set; }

        [Required]
        // [EmailAddress]
        [MaxLength(50)]
        [Display(Name = "Email ")]
        public string MailId { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Mobile number length should be 9 digit required")]
        [MinLength(9)]
        [MaxLength(9)]
        [Display(Name = "Mobile Number ")]
        public string MobileNo { get; set; }



        //[Required]
        [Display(Name = "Role ")]
        public int? RoleId { get; set; }

        public List<SelectListItem>? Roles { get; set; }

        [Display(Name = "Status ")]
        public string Status { get; set; }


    }
}
