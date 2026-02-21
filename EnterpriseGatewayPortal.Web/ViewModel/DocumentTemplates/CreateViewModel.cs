using DocumentFormat.OpenXml.Wordprocessing;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates
{
    public class CreateViewModel
    {
        [Required]
        [Display(Name = "Template Name")]
        public string? TemplateName { get; set; }

        [Required]
        [Display(Name = "Document Name")]
        public string? DocumentName { get; set; }

        [Required]
        public string? Config { get; set; }

        public string? Signatory { get; set; }

        public int SignatureTemplate { get; set; }

        public int EsealSignatureTemplate { get; set; }

        public int Rotation { get; set; }
        public string? SettingConfig { get; set; }
        public BulkSignerListViewModel BulkSignerEmails { get; set; }

        public IList<Roles> RoleList { get; set; }

        [Required]
        [Display(Name = "Upload Document")]
        public IFormFile File { get; set; }

        public IList<RescpList> RecpientList { get; set; }

        public IEnumerable<SignatureTemplate> TemplateList { get; set; }

        public string annotations { get; set; }

        public string esealAnnotations { get; set; }

        public string qrCodeAnnotations { get; set; }

        public bool qrCodeRequired { get; set; }

        public bool esealRequired { get; set; }

        public string settingConfig { get; set; }
        public string htmlSchema { get; set; }


        public IList<string> emailList { get; set; }

        public int signatureTemplate { get; set; }

        public int esealSignatureTemplate { get; set; }
        public int rotation { get; set; }

        public string status { get; set; }

        public string edmsId { get; set; }

        public string _id { get; set; }

        public string createdBy { get; set; }

        public string updatedBy { get; set; }
        
        public string fileKb { get; set; }

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

