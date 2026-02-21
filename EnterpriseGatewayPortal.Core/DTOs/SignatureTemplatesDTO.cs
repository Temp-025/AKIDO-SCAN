using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SignatureTemplatesDTO
    {
        public int Id { get; set; }

        public string TemplateId { get; set; }

        public string Type { get; set; }

        public string DisplayName { get; set; }

        public string SamplePreview { get; set; }
    }
}
