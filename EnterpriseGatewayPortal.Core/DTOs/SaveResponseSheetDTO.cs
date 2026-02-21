using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SaveResponseSheetDTO
    {
        public string fileContents { get; set; }
        public string contentType { get; set; }
        public string FileDownloadName { get; set; }
        public string lastModified { get; set; }
        public string entityTag { get; set; }
        public bool enableRangeProcessing { get; set; }
    }
}
