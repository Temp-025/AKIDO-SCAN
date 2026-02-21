using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class BeneficiariesServicesDTO
    {
        public int privilegeId { get; set; }

        public string privilegeServiceName { get; set; }
        public string privilegeServiceDisplayName { get; set; }

        public string status { get; set; }
        public int isChargeable { get; set; }
    }
}
