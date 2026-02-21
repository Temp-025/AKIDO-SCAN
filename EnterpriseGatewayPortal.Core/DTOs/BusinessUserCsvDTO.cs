using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class BusinessUserCsvDTO
    {
        public IList<BusinessUserList> trustedBusinessUserDtosList { get; set; }
    }
    public class BusinessUserList
    {
        [Display(Name = "Employee Email")]
        public string? employee_email { get; set; }

        [Display(Name = "Designation")]
        public string? designation { get; set; }

        [Display(Name = "Signature Photo")]
        public string? signature_photo { get; set; }

        [Display(Name = "UAEID Email")]
        public string? ugpass_email { get; set; }

        [Display(Name = "Passport Number")]
        public string? passport_number { get; set; }

        [Display(Name = "National Id Number")]
        public string? national_id_number { get; set; }

        [Display(Name = "Mobile Number")]
        public string? mobile_number { get; set; }

        [Display(Name = "Privileges")]
        public List<string> privileges { get; set; }
        public bool is_org_signatory { get; set; }
    }
}
