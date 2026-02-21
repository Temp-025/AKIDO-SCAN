using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class UserSessionObj
    {
        public string Uuid { get; set; }
        public string fullname { get; set; }
        public string dob { get; set; }
        public string mailId { get; set; }
        public int sub { get; set; }
        public string mobileNo { get; set; }
        public List<string> AccessibleModule { get; set; }
        public IList<LoginProfile> LoginProfile { get; set; }
    }
    public class LoginProfile
    {
        public string Email { get; set; }
        public string OrgnizationId { get; set; }

    }
}
