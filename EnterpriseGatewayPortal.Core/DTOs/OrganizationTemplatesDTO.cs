using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class OrganizationTemplatesDTO
    {
        public string organizationUid { get; set; }
        public int signatureTemplateId { get; set; }
        public int esealSignatureTemplateId { get; set; }
    }
}
