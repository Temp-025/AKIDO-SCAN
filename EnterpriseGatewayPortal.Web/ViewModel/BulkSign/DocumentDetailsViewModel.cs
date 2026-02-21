using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.BulkSign
{
    public class DocumentDetailsViewModel
    {
        public string correlationId { get; set; }
        public string DestinationPath { get; set; }
        public Result1 result { get; set; }
    }

}
