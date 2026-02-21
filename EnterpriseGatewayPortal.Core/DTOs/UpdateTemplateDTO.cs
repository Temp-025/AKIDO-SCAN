using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class UpdateTemplateDTO
    {
        public IEnumerable<SignatureTemplate> TemplateList { get; set; }
        public int SignatureTemplate { get; set; }
        public int ESealTemplate { get; set; }
    }
}
