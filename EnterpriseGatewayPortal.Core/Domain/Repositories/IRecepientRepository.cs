using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IRecepientRepository : IGenericRepository<Recepient>
    {
        Task<Recepient?> DeclinedCommentDetailsAsync(string tempId);
        Task<bool> DeclineSigningAsync(string tempId, string suid, string comment);
        Task DeleteRecepientsByTempId(IList<string> idList);
        Task<Recepient?> FindRecepientsByCorelationId(string corelationId);
        Task<IList<Recepient>> GetCurrentRecepientsList(string tempId);
        Task<Recepient?> GetRecepientsBySuidAndTempId(string suid, string tempId);
        Task<IList<Recepient>> GetRecepientsBySuidAsync(string suid);
        Task<IList<Recepient>> GetRecepientsLeft(string tempId);
        Task<IList<Recepient>> GetRecepientsListByDocIdAsync(string id);
        Task<IList<Recepient>> GetRecepientsListByTakenActionAsync(string suid, bool action);
        Task<IList<Recepient>> GetRecepientsListByTempIdAsync(Recepient recepients);
        Task<Recepient> SaveReceipt(Recepient recepients);
        Task<IList<Recepient>> SaveRecepientsAsync(IList<Recepient> recepients);
        Task<bool> UpdateRecepientsById(Recepient recepients);
        Task<bool> UpdateTakenActionOfRecepientById(string id);
    }
}
