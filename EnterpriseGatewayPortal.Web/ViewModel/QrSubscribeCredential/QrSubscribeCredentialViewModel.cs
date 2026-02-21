using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.ViewModel.DocumentTemplates;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnterpriseGatewayPortal.Web.ViewModel.QrSubscribeCredential
{
    public class QrSubscribeCredentialViewModel
    {

        public int id { get; set; }
        public string? credentialId { get; set; }
        public string? organizationId { get; set; }
        public string? credentialName { get; set; }
        public string? organizationName { get; set; }
        public QrAttributesDTO attributes { get; set; }
        public List<string> emails { get; set; }
        public string? status { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public string? remarks { get; set; }

        public BulkSignerListViewModel BulkSignerEmails { get; set; }
       
        public string? CredentialName { get; set; }
        public List<SelectListItem> CredentialNameList { get; set; }

        public List<string> Selectedemails { get; set; }

    }
}
