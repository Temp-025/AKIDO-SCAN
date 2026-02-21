using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class ExistingCreditsDTO
    {
        public bool PostPaid { get; set; }
        public double Eseal_SIGNATURE { get; set; }
        public double Digital_SIGNATURE { get; set; }
        public double User_SUBSCRIPTION { get; set; }
    }
}
