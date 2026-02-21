using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class AgentUrlAndSpocUpdateDTO
    {
        public string OrganizationUid { get; set; }
        public string? AgentUrl { get; set; }
        public string? SpocUgpassEmail { get; set; }
    }
}
