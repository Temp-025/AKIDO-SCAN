using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class ServicesBySUIDDTO
    {
        

        public RequestBody requestBody { get; set; }
        public string serviceMethod { get; set; }
    }

    public class RequestBody
    {
        public string Suid { get; set; }
    }
}
