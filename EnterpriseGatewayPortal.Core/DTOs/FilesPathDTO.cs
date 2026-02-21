using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class FilesPathDTO
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public string TransactionName { get; set; }
        public string TemplateId { get; set; }
    }
}
