using EnterpriseGatewayPortal.Core.Constants;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class DocumentRepository : GenericRepository<Document, EnterprisegatewayportalDbContext>, IDocumentRepository
    {
        private readonly ILogger _logger;
        public DocumentRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<Document?> GetDocumentById(string id)
        {
            var document = await Context.Documents.AsNoTracking()
                .Where(x => x.DocumentId == id)
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.Recepients)
                .FirstOrDefaultAsync();

            return document;
        }

        public async Task<IList<Document>> GetDocumentListAsync(string suid, string accountType, string organizationId)
        {
            var documents = await Context.Documents.AsNoTracking()
                .Where(x => x.OwnerId == suid && x.AccountType == accountType &&
                            (accountType != AccountTypeConstants.Organization || x.OrganizationId == organizationId))
                .OrderByDescending(x => x.UpdatedAt)
                .Include(x => x.Recepients) // Assuming Recepients is a navigation property in Document entity
                .ToListAsync();

            return documents;
        }

        public async Task<Document> SaveDocument(Document document)
        {
            Context.Documents.Add(document);
            await Context.SaveChangesAsync();
            return document;
        }

        public async Task DeleteDocumentsByIdsAsync(IList<string> idList)
        {
            var documentsToDelete = await Context.Documents
                .Where(x => idList.Contains(x.DocumentId))
                .ToListAsync();

            Context.Documents.RemoveRange(documentsToDelete);
            await Context.SaveChangesAsync();
        }

        public async Task<bool> UpdateDocumentStatusById(Document document)
        {
            var existingDocument = await Context.Documents
                .FirstOrDefaultAsync(x => x.DocumentId == document.DocumentId);

            if (existingDocument != null)
            {
                existingDocument.CompleteTime = document.CompleteTime;                
                existingDocument.Status = document.Status;
                existingDocument.UpdatedAt = document.UpdatedAt;
                existingDocument.PendingSignList = document.PendingSignList;
                existingDocument.CompleteSignList = document.CompleteSignList;

                int changes = await Context.SaveChangesAsync();

                // Check if any modifications were made during SaveChanges
                return changes > 0;
            }

            return false;
        }

        public async Task<bool> UpdateDocumentBlockedStatusAsync(string id, bool blockedStatus)
        {
            var document = await Context.Documents
                .FirstOrDefaultAsync(x => x.DocumentId == id);

            if (document != null)
            {
                document.IsDocumentBlocked = blockedStatus;
                document.DocumentBlockedTime = DateTime.Now;

                await Context.SaveChangesAsync();

                // Check if any modifications were made during SaveChanges
                return Context.Entry(document).State == EntityState.Modified;
            }

            return false;
        }

    }
}
