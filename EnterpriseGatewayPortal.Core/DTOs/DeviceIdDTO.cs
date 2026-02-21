using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DeviceIdDTO
    {
        public int id { get; set; }
        public string clientId { get; set; }
        public string organizationName { get; set; }
        public string applicationName { get; set; }
        public string deviceId { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
    }
}
