using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class UpdateDocumentTemplateDTO
    {
        public IFormFile File { get; set; }
        public string Model { get; set; }
        public string TemplateId { get; set; }
    }

}
