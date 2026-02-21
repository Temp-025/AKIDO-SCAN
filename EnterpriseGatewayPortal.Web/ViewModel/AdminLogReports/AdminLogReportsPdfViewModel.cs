using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Utilities;

namespace EnterpriseGatewayPortal.Web.ViewModel.AdminLogReports
{
    public class AdminLogReportsPdfViewModel
    {
        public PaginatedList<AdminLogReportDTO> AdminLogReports { get; set; }
    }
}
