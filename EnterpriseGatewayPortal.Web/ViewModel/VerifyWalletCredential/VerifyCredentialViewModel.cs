namespace EnterpriseGatewayPortal.Web.ViewModel.VerifyWalletCredential
{
	    public class VerifyCredentialViewModel
	{
		
			public int id { get; set; }
			public string? credentialId { get; set; }
			public string? organizationId { get; set; }
			public List<DataAttributes> attributes { get; set; }
			public List<CredentialConfig> configuration { get; set; }
			public List<string> emails { get; set; }
			public string? status { get; set; }
			public DateTime? createdDate { get; set; }
			public DateTime? updatedDate { get; set; }
		}
		
	}

