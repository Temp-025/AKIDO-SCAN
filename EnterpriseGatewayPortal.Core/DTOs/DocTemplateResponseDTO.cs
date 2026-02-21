using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DocTemplateResponseDTO
    {
        public DocumentTemplateDTO Template { get; set; }
        public DigitalFormResponseDTO FormResponse { get; set; }
    }
}
