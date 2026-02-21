using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class RevokeCredentialDTO
    {
        public string? CredentialId { get; set; }
        public string? DocumentId { get; set; }
    }
}
