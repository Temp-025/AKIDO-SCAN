using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class GetDocumentTemplatesListDTO
    {
        public List<Template> templates { get; set; }
        public List<Subscriberorgtemplate> subscriberOrgTemplates {  get; set; }
    }
}
