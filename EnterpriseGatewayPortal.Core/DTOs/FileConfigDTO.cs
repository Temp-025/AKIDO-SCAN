using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class FileConfigDTO
    {
        public int NumberOfFiles { get; set; }
        public int EachFileSize { get; set; }
        public int AllFileSize { get; set; }
    }
}
