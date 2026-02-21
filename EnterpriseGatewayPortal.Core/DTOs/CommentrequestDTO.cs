using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class CommentrequestDTO
    {
        public IFormFile File { get; set; }
        public string Comments { get; set; }
    }
}
