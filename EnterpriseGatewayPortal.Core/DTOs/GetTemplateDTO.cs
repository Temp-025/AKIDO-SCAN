using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class GetTemplateDTO
    {
        public string Email { get; set; }

        public string OrgId { get; set; }

        public int TemplateId { get; set; }
    }
}
