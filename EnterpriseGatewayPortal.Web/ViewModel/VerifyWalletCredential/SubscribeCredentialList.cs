using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnterpriseGatewayPortal.Web.ViewModel.VerifyWalletCredential
{
	public class SubscribeCredentialList
	{
		public string? CredentialName { get; set; }
		public List<SelectListItem> CredentialNameList { get; set; }
	}
}
