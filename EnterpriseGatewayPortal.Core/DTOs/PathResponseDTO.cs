using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class PathResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Result { get; set; }
    }
}
