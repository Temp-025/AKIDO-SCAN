using EnterpriseGatewayPortal.Web.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.ESealDetails
{
    public class ESealDetailsViewModel
    {
        [Display(Name = "Eseal Certificate Status")]
        public string certificateStatus { get; set; }
        [Display(Name = "Certificate Start Date")]
        public string certificateStartDate { get; set; }
        [Display(Name = "Certificate Expire Date")]
        public string certificateEndDate { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(100 * 1024)] // 100kb
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        [Display(Name = "E-seal Image")]
        public IFormFile ESealImage { get; set; }

        public string? ResizedEsealImage { get; set; }

        [Display(Name = "Eseal Logo")]
        public string? ESealImageBase64 { get; set; }
    }
}
