using DocumentFormat.OpenXml.Wordprocessing;
using EnterpriseGatewayPortal.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.Priceslabs
{
    public class GenericPriceSlabDetailsViewModel
    {
        public GenericPriceSlabDetailsViewModel()
        {
            DiscountVolumeRanges = new List<DiscountVolumeRangeDTO>();
        }

        [Required(ErrorMessage = "Select a service")]
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        [Required(ErrorMessage = "Select Service")]
        [Display(Name = "Service")]
        public string ServiceDisplayName { get; set; }

        public IList<DiscountVolumeRangeDTO> DiscountVolumeRanges { get; set; }

        [Required(ErrorMessage = "Select stakeholder")]
        [Display(Name = "Stakeholder")]
        public string Stakeholder { get; set; }

        public string CreatedBy { get; set; }

        public string Updatedby { get; set; }

        public string OrganizationUID { get; set; }

        [Display(Name = "Organization")]
        public string OrganizationName { get; set; }
    }
}
