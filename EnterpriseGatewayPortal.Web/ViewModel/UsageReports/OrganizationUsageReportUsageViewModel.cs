using DocumentFormat.OpenXml.Wordprocessing;
using EnterpriseGatewayPortal.Core.DTOs;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.UsageReports
{
    public class OrganizationUsageReportUsageViewModel
    {
        [Required(ErrorMessage = "Select year")]
        [Display(Name = "Year")]
        public string Year { get; set; }
       

       // public IEnumerable<OrganizationUsageReportDTO> OrganizationUsageReports { get; set; }

        public IList<OrgUsageReportsViewModel> OrgUsageReports { get; set; }
    }

    public class OrgUsageReportsViewModel
    {
        public int Id { get; set; }

        public string OrgName { get; set; }

        public string OrganizationId { get; set; }

        public string ReportMonth { get; set; }

        public string ReportYear { get; set; }

        public double TotalInvoiceAmount { get; set; }

        public string CreatedOn { get; set; }
    }
}
