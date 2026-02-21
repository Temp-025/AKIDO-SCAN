using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnterpriseGatewayPortal.Web.ViewModel.QrSubscribeCredential
{
    public class SubscribeQrCredentialList
    {
        public string? CredentialName { get; set; }
        public List<SelectListItem> CredentialNameList { get; set; }
    }

}
