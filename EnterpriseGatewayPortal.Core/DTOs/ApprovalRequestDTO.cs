using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class ApprovalRequestDTO
    {
        public string credentialId { get; set; }
        public string signedDocument { get; set; }
    }
}
