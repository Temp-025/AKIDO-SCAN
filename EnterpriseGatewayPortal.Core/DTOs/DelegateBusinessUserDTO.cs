using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DelegateBusinessUserDTO
    {
        public string FullName { get; set; }
        public string Suid { get; set; }
        public string Email { get; set; }
        public string ThumbNailUri { get; set; }
        public string OrganizationEmail { get; set; }
        public bool HasEseal { get; set; }
    }
}
