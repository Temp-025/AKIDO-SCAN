using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class DownloadVerifyDocumentDTO
    {
        public int Id { get; set; }

        public string? FileId { get; set; }

        public string? FileName { get; set; }

        public byte[]? File { get; set; }

        public byte[]? VerificationReport { get; set; }
    }
}
