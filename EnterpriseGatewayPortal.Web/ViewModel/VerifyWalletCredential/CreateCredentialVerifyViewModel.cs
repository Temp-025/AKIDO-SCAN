namespace EnterpriseGatewayPortal.Web.ViewModel.VerifyWalletCredential
{
	public class CreateCredentialVerifyViewModel
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
		public class DataAttributes
		{

			public string? displayName { get; set; }
			public string? attribute { get; set; }
			public int dataType { get; set; }
			public bool mandatory { get; set; }

		}
		public class CredentialConfig
		{
			public string? format { get; set; }
			public string? bindingMethod { get; set; }
			public string? supportedMethod { get; set; }
		}
}

