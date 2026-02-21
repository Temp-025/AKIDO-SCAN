using DocumentFormat.OpenXml.Wordprocessing;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.Documents
{
    public class DocumentCreateViewModel
    {

        [Required]
        [Display(Name = "Document Name")]
        public string? DocumentName { get; set; }

        [Required]
        public string? Config { get; set; }

        public string? Signatory { get; set; }
        public string? Recps { get; set; }

        public string? InitialBaseString { get; set; }
        public string docSerialNo { get; set; } = string.Empty;
        public string entityName { get; set; } = string.Empty;
        public bool faceRequired { get; set; } = false;

        public int Rotation { get; set; }
        public string? SettingConfig { get; set; }
      
        public IList<Roles> RoleList { get; set; }

        [Required]
        [Display(Name = "Upload Document")]
        
       
        public IFormFile File { get; set; }

        public IList<RescpList> RecpientList { get; set; }

        

        public string annotations { get; set; }

        public string esealAnnotations { get; set; }

        public string qrCodeAnnotations { get; set; }

        public bool qrCodeRequired { get; set; }

        public string settingConfig { get; set; }

        public IList<Roles> roleList { get; set; }

        public IList<string> emailList { get; set; }

      
        public int rotation { get; set; }

        public string status { get; set; }

        public string edmsId { get; set; }

        public string _id { get; set; }

        public string createdBy { get; set; }

        public string updatedBy { get; set; }
        public string htmlSchema { get; set; }

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
