using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DigitalFormResponseDTO
    {
        public string FormId { get; set; }

        public string FormTemplateName { get; set; }

        public string CorelationId { get; set; }

        public string Status { get; set; }

        public string SignerName { get; set; }

        public string SignerEmail { get; set; }

        public string SignerSuid { get; set; }

        public string FormFieldData { get; set; }

        public string AcToken { get; set; }

        public string EdmsId { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public virtual string _id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
