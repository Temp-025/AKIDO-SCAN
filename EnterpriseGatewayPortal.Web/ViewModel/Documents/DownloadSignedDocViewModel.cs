using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Web.ViewModel.Documents
{
    public class DownloadSignedDocViewModel
    {
        public int Id { get; set; }

        public string FileId { get; set; } = null!;

        public byte[]? File { get; set; }

        public virtual ICollection<Document> Documents { get; } = new List<Document>();
    }
}
