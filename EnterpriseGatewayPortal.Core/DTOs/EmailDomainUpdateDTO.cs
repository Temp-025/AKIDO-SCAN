using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class EmailDomainUpdateDTO
    {
        public string OrganizationUid { get; set; }
        public string? EmailDomain { get; set; }
    }
}
