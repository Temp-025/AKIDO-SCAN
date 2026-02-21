using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.ViewModel.Documents
{
    public class SignDocumentViewModel
    {
        [Required]
        public string? Config { get; set; }

        public IFormFile File { get; set; }
        public string? Signatory { get; set; }
        public string? DocId { get; set; }

    }
}
