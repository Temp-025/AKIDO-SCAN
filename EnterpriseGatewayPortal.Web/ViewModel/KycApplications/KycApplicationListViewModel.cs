using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.KycApplications
{
    public class KycApplicationListViewModel
    {
        public List<KycApplicationDTO> KycApplications { get; set; }
        public string? ApplicationName { get; set; }

        public string? ApplicationType { get; set; }

        public string? Status { get; set; }
    }
}
