using DocumentFormat.OpenXml.Wordprocessing;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.VerificationRequests
{
    public class DownloadDocViewModel
    {
        public int Id { get; set; }

        public string? FileId { get; set; }

        public byte[]? File { get; set; }

        public byte[]? VerificationReport { get; set; }

        public int RequestId { get; set; }
       
        [Required]
        public string? Config { get; set; }

        public string? Signatory { get; set; }

        public string? FileName { get; set; }
        public IList<Roles> RoleList { get; set; }

        [Required]
        [Display(Name = "Upload Document")]
        public IFormFile FileData { get; set; }

        public IList<RescpList> RecpientList { get; set; }

       
        public string annotations { get; set; }

        public string esealAnnotations { get; set; }

        public string qrCodeAnnotations { get; set; }

        public bool qrCodeRequired { get; set; }

        public bool esealRequired { get; set; }

        public string settingConfig { get; set; }



        public IList<string> emailList { get; set; }

        public int signatureTemplate { get; set; }

        public int esealSignatureTemplate { get; set; }
        public int rotation { get; set; }

        public string status { get; set; }

        public string edmsId { get; set; }

        public string _id { get; set; }

        public string createdBy { get; set; }

        public string updatedBy { get; set; }

        public int pageHeight { get; set; }
        public int pageWidth { get; set; }

    }

    public class RescpList
    {
        public int Order { get; set; }
        public string? Email { get; set; }

        public bool Eseal { get; set; }
    }

    public class Roles
    {
        public int Order { get; set; }

        public string Role { get; set; }

        public bool Eseal { get; set; }
    }


}

