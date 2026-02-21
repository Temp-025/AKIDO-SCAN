using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class KYCLogReportDTO
    {
        public string _id { get; set; }
        public string ModuleName { get; set; }
        public string ServiceName { get; set; }
        public string ActivityName { get; set; }
        public string Timestamp { get; set; }
        public string LogMessage { get; set; }
        public string LogMessageType { get; set; }
        public string UserName { get; set; }
        public string DataTransformation { get; set; }
        public string Checksum { get; set; }
        public bool IsChecksumValid { get; set; }
        public int __v { get; set; }
    }

    public class KycTransResponseDTO
    {
        [JsonProperty("result")]
        public List<LogReportDTO> Result { get; set; }

        [JsonProperty("perPage")]
        public int PerPage { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class VerifiedIdValidationResponseDTO
    {
        public string name { get; set; }
        public string nationality { get; set; }
        public string idNumber { get; set; }
        public string issueDate { get; set; }
        public string expiryDate { get; set; }
        public string photo { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string documentStatus { get; set; }

    }

    public class ValidateSignedDataRequestDTO
    {
        public string SignedData { get; set; }
        public string KycMethod { get; set; }
    }

    public class BatchKYCResponseWrapper
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public List<KYCValidationResponseDTO> Result { get; set; }
    }



}
