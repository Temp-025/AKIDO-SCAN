using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class FileStatusResponseDTO
    {
        public string CorrelationId { get; set; }
        public ResultDTO Result { get; set; }
    }

    public class FileSigningStatusDTO
    {
        public string FileName { get; set; }
        public string Status { get; set; }
    }

    public class ResultDTO
    {
        public int TotalFileCount { get; set; }
        public int FailedFileCount { get; set; }
        public int SuccessFileCount { get; set; }
        //  public List<FileSigningStatusDTO> FileSigningStatus { get; set; }
        public List<FileBulkSigningStatusDTO> FileSigningStatus { get; set; }
    }

    public class FileBulkSigningStatusDTO
    {
        public string FileName { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
