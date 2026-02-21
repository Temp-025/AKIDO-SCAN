using System.ComponentModel.DataAnnotations;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnterpriseGatewayPortal.Web.ViewModel.Wallet
{
	public class WalletConfigurationViewModel
	{
		public int Id { get; set; }
		public string? format { get; set; }
		public string? bindingMethod { get; set; }
		public string? supportedMethod { get; set; }
		public List<DataAttribute> DataAttributes { get; set; }
		public BulkSignerListViewModel BulkSignerEmails { get; set; }
		public string? credentialId { get; set; }
		public string? organizationId { get; set; }

		public string? CredentialName { get; set; }
		public List<SelectListItem> CredentialNameList { get; set; }
		public List<string> Selectedemails { get; set; }
		public List<CredentialConfig> Selectedconfiguration { get; set; }
		public List<WalletConfigurationDTO> Originalconfiguration { get; set; }

		public string? status {  get; set; }
     
        public string? Domain { get; set; }

        public List<SelectListItem> DomainsList { get; set; }

        public Dictionary<string, string> purposes { get; set; }

        public List<WalletDomainDTO> DomainConsent { get; set; }



    }
}
