using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class KycDeviceBlockDTO
    {
        public string OrganizationId { get; set; }
        public string ClientId { get; set; }
        public string DeviceId { get; set; }
    }
}
