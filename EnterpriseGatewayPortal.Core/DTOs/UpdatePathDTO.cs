using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class UpdatePathDTO
    {
        public string CorelationId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
