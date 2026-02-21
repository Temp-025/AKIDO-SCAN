using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class QrTestCredentialDTO
    {

        public string? CredentialId { get; set; }
        public Data Data { get; set; }

    }
        public class Data
        {
            public List<object> publicData { get; set; }
            public List<object> privateData { get; set; }
        }
    
}
