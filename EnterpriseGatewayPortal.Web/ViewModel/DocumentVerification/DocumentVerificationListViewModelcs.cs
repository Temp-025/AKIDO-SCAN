using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.DocumentVerification
{
    public class DocumentVerificationListViewModelcs
    {
        public IEnumerable<DocumentVerifyListDTO> DocumentVerifyLists { get; set; } = new List<DocumentVerifyListDTO>();
    }
}
