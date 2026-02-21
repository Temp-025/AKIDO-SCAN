using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Utilities;

namespace EnterpriseGatewayPortal.Web.ViewModel.KYCDashboard
{
    public class KYCDashboardViewModel
    {
        public PaginatedList<KYCValidationResponseDTO> Report { get; set; }
        public OrgKycSummaryDTO orgReport { get; set; }
        public int NoOfKycDevices { get; set; }

        public string OrgID { get; set; }

    }
}