using EnterpriseGatewayPortal.Core.DTOs;
namespace EnterpriseGatewayPortal.Web.ViewModel.VerifyWalletCredential
{
	public class VerifyCredentialListViewModel
	{
		
			public int id { get; set; }
			public string credentialId { get; set; }
			public string organizationId { get; set; }
			public string? credentialName { get; set; }
			public string? organizationName { get; set; }
			public List<DataAttributes> attributes { get; set; }
			public List<CredentialConfig> configuration { get; set; }
			public List<string> emails { get; set; }
			public string status { get; set; }
			public DateTime createdDate { get; set; }
			public DateTime updatedDate { get; set; }
		}
		
	}

