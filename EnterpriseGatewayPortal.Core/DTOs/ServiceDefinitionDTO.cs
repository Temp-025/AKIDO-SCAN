using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class ServiceDefinitionDTO
    {
        public int Id { get; set; }

        public string ServiceName { get; set; }

        public string ServiceDisplayName { get; set; }

        public string Status { get; set; }

        public bool PricingSlabApplicable { get; set; }
    }
}
