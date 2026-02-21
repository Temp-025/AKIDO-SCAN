using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class LicenceDTO
    {
        public string clientId { get; set; }
        public string licenseType { get; set; }
        public string orgId { get; set; }
        public string macAddress { get; set; }
    }
}
