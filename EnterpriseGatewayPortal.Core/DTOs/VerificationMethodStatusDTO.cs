using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class VerificationMethodStatusDTO
    {
        public int Id { get; set; }
        public string MethodCode { get; set; }
        public string MethodUid { get; set; }

        public string MethodType { get; set; }

        public string MethodName { get; set; }

        public decimal Pricing { get; set; }

        public string ProcessingTime { get; set; }

        public string ConfidenceThreshold { get; set; }
        public List<string> TargetSegments { get; set; }

        public List<string> MandatoryAttributes { get; set; }

        public List<string> OptionalAttributes { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
        public string RequestStatus { get; set; }
        public bool Requested { get; set; }
        public string RequestedDate { get; set; }
        public string ModifiedDate { get; set; }
    }
}
