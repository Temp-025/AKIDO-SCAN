using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class GetRateCardDTO
    {
        public RequestBodyDto1 requestBody { get; set; }
        public string serviceMethod { get; set; }
    }
    public class RequestBodyDto1
    {
        public string OrgId { get; set; }
        public string serviceId { get; set; }
    }
}
