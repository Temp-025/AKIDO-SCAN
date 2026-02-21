using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SignatureAnnotation
    {
        public string fieldName { get; set; }
        public double posX { get; set; }
        public double posY { get; set; }
        public int PageNumber { get; set; }
        public double width { get; set; }
        public double height { get; set; }
    }

    public class ESealAnnotation
    {
        public string fieldName { get; set; }
        public double posX { get; set; }
        public double posY { get; set; }
        public int PageNumber { get; set; }
        public double width { get; set; }
        public double height { get; set; }
        public string organizationID { get; set; }
    }

    public class FileResult
    {
        public string fileName { get; set; }
        public string status { get; set; }
    }

    public class Result1
    {
        public int totalFileCount { get; set; }
        public int failedFileCount { get; set; }
        public int successFileCount { get; set; }
        public List<FileResult> fileArray { get; set; }
    }

    public class DocumentDetailsDTO
    {
        public string templateId { get; set; }
        public string templateName { get; set; }
        public string organizationId { get; set; }
        public string suid { get; set; }
        public string signatureAnnotations { get; set; }
        public string esealAnnotations { get; set; }
        public string sourcePath { get; set; }
        public string signedPath { get; set; }
        public string status { get; set; }
        public string corelationId { get; set; }
        public object signedBy { get; set; }
        public object completedAt { get; set; }
        public string ownerName { get; set; }
        public string ownerEmail { get; set; }
        public object signerEmail { get; set; }
        public Result1 result { get; set; }
        public string _id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
