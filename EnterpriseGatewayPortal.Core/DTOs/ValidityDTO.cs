using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class ValidityDTO
    {
        public string? privilegeId { get; set; }
        public string? validityCheckbox { get; set; }
        public DateOnly? validityFrom { get; set; }
        public DateOnly? validityUpto { get; set; }
    
    }
}
