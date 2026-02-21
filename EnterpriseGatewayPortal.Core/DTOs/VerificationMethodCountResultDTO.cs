using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class VerificationMethodCountResultDTO
    {
        public int Total { get; set; }
        public int Active { get; set; }
        public int Pending { get; set; }
        public int Rejected { get; set; }
    }
}
