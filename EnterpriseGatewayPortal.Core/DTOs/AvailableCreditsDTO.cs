using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class AvailableCreditsDTO
    {
        public RequestBodyDto requestBody { get; set; }
        public string serviceMethod { get; set; }
    }
    public class RequestBodyDto
    {
        public string OrgId { get; set; }
    }
}
