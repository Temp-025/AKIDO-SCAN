using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Services.Communication.Documents
{
    public class AllDocumentsResponse
    {
        public IList<Document> data { get; set; } = null;

        public int allowed_no_of_files { get; set; }
    }
}
