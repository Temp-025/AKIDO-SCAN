using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DownloadDocDTO
    {
        public int id { get; set; }
        public string fileId { get; set; }
        public List<string> documents { get; set; }
        public byte[] file { get; set; }
    }
}
