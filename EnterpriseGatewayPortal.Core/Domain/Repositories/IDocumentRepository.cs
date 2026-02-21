using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        Task DeleteDocumentsByIdsAsync(IList<string> idList);
        Task<Document?> GetDocumentById(string id);
        Task<IList<Document>> GetDocumentListAsync(string suid, string accountType, string organizationId);
        Task<Document> SaveDocument(Document document);
        Task<bool> UpdateDocumentBlockedStatusAsync(string id, bool blockedStatus);
        Task<bool> UpdateDocumentStatusById(Document document);
    }
}
