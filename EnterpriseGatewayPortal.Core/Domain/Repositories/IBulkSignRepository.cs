
using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IBulkSignRepository : IGenericRepository<Bulksign>
    {
        Task<IList<Bulksign>> GetBulkSigDataList(string orgId, string suid);
        Task<IList<Bulksign>> GetBulkSigDataListByTemplateId(string templateID);
        Task<Bulksign?> GetBulkSignData(int id);
        Task<Bulksign?> GetBulkSignDataByCorelationId(string corelationId);
        Task<IList<Bulksign>> GetReceivedBulkSignDataList(string orgId, string signerEmail);
        Task<IList<Bulksign>> GetSentBulkSignDataList(string orgId, string suid);
        Task<bool> IsBulkSigningTransactionNameExists(string transactionName);
        Task<Bulksign> SaveBulkSignData(Bulksign bulkSign);
        Task<bool> UpdateBulkSignData(Bulksign bulkSign);
        Task<bool> UpdateBulkSignSrcDestData(Bulksign bulkSign);
    }
}
