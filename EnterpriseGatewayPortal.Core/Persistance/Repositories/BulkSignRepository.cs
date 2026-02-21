using EnterpriseGatewayPortal.Core.Constants;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class BulkSignRepository : GenericRepository<Bulksign, EnterprisegatewayportalDbContext>, IBulkSignRepository
    {
        private readonly ILogger _logger;
        public BulkSignRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<bool> IsBulkSigningTransactionNameExists(string transactionName)
        {
            var count = await Context.Bulksigns
                .Where(x => x.Transaction == transactionName)
                .CountAsync();

            return count > 0;
        }

        public async Task<Bulksign> SaveBulkSignData(Bulksign bulkSign)
        {
            await Context.Bulksigns.AddAsync(bulkSign);
            await Context.SaveChangesAsync();
            return bulkSign;
        }

        public async Task<IList<Bulksign>> GetBulkSigDataList(string orgId, string suid)
        {
            return await Context.Bulksigns
                .Where(x => x.Suid == suid && x.OrganizationId == orgId && x.SignerEmail == null)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IList<Bulksign>> GetSentBulkSignDataList(string orgId, string suid)
        {
            return await Context.Bulksigns
                .Where(x => x.Suid == suid && x.OrganizationId == orgId && x.SignerEmail != null)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IList<Bulksign>> GetReceivedBulkSignDataList(string orgId, string signerEmail)
        {
            return await Context.Bulksigns
                .Where(x => x.SignerEmail == signerEmail && x.OrganizationId == orgId && x.Status != DocumentStatusConstants.Draft)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<IList<Bulksign>> GetBulkSigDataListByTemplateId(string templateID)
        {
            return await Context.Bulksigns
                .Where(x => x.TemplateId == templateID && x.Status != DocumentStatusConstants.Draft)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Bulksign?> GetBulkSignDataByCorelationId(string corelationId)
        {
            return await Context.Bulksigns
                .Where(x => x.CorelationId == corelationId)
                .FirstOrDefaultAsync();
        }

        public async Task<Bulksign?> GetBulkSignData(int id)
        {
            return await Context.Bulksigns.FindAsync(id);
        }

        public async Task<bool> UpdateBulkSignData(Bulksign bulkSign)
        {
            var entity = await Context.Bulksigns
                .Where(x => x.CorelationId == bulkSign.CorelationId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.Result = bulkSign.Result;
                entity.Status = bulkSign.Status;

                await Context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateBulkSignSrcDestData(Bulksign bulkSign)
        {
            var entity = await Context.Bulksigns
                .Where(x => x.CorelationId == bulkSign.CorelationId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.SourcePath = bulkSign.SourcePath;
                entity.SignedPath = bulkSign.SignedPath;

                await Context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
