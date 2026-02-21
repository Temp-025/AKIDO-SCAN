using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class OrganizationVerificationMethodDTO
    {
        public string VerificationMethodUid { get; set; }
        public string OrganizationId { get; internal set; }
    }
}
