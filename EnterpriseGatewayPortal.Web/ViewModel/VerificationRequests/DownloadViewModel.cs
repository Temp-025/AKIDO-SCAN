using DocumentFormat.OpenXml.Wordprocessing;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.VerificationRequests
{
    public class DownloadViewModel
    {
        public int Id { get; set; }

        public string? FileId { get; set; }
        public string? FileName { get; set; }

        public byte[]? File { get; set; }

        public byte[]? VerificationReport { get; set; }

    }
}

