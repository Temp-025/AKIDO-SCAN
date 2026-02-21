using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class VerificationRequestTrueCopySignDTO
    {
      
            public int RequestId { get; set; }

            public string? OrgId { get; set; }

            public string? OrgName { get; set; }

            public string? VerifierName { get; set; }

            public string? Email { get; set; }

            public string? Suid { get; set; }

            public string? Annotations { get; set; }

            public string? ESealAnnotation { get; set; }

            public string? StampAnnotation { get; set; }

            public string? FileName { get; set; }
       
    }

    public class TrueCopyCoordinatesdata
    {
        public string pageNumber { get; set; }

        public string signatureXaxis { get; set; }

        public string signatureYaxis { get; set; }

        public string imgWidth { get; set; }

        public string imgHeight { get; set; }
    }

    public class EsealCoordinatesdata
    {
        public string pageNumber { get; set; }

        public string signatureXaxis { get; set; }

        public string signatureYaxis { get; set; }

        public string imgWidth { get; set; }

        public string imgHeight { get; set; }
    }

    public class SignatoryCoordinatesdata
    {
        public string pageNumber { get; set; }

        public string signatureXaxis { get; set; }

        public string signatureYaxis { get; set; }

        public string imgWidth { get; set; }

        public string imgHeight { get; set; }
    }

}
