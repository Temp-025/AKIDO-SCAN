using DocumentFormat.OpenXml.Wordprocessing;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.DocumentVerification
{
    public class CreateVerificationViewModelcs
    {
        public string? IssuerUid { get; set; }
        [Display(Name = "Issuer Name")]
        public string? IssuerName { get; set; }

        [Display(Name = "Document Type")]
        public string? DocumentType { get; set; }

        [Required]
        [Display(Name = "Verification Method")]
        public string? VerificationMethod { get; set; }

        [Required]
        [Display(Name = "Upload Document")]
        public IFormFile File { get; set; }

      //  public List<DocumentIssuerListDTO> DocumentDetails { get; set; }

        public List<IssuerOrgNamesDTO> IssuerOrgNameDetails { get; set; }

        
    }
}
