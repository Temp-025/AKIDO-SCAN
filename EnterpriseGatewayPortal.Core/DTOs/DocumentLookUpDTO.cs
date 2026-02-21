using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DocumentLookUpDTO
    {
        public int Id { get; set; }
        public string DocumentName { get; set; } = null!;
        public string SignatoryList { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
