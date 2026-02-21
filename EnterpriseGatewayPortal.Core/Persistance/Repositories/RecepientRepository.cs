//using EnterpriseGatewayPortal.Core.Constants;
//using EnterpriseGatewayPortal.Core.Domain.Models;
//using EnterpriseGatewayPortal.Core.Domain.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

//namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
//{
//    public class RecepientRepository : GenericRepository<Recepient, EnterprisegatewayportalDbContext>, IRecepientRepository
//    {
//        private readonly ILogger _logger;
//        public RecepientRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
//        {
//            _logger = logger;
//        }

//        public async Task<IList<Recepient>> GetRecepientsListByTakenActionAsync(string suid, bool action)
//        {
//            return await Context.Recepients
//                .Where(x => x.Suid == suid && x.Takenaction == action)
//                .ToListAsync();
//        }

//        public async Task<IList<Recepient>> GetRecepientsBySuidAsync(string suid)
//        {
//            return await Context.Recepients
//                .Where(x => x.Suid == suid)
//                .ToListAsync();
//        }

//        public async Task<IList<Recepient>> GetRecepientsListByDocIdAsync(string id)
//        {
//            return await Context.Recepients
//                .Where(x => x.Tempid == id)
//                .ToListAsync();
//        }

//        public async Task<IList<Recepient>> GetRecepientsListByTempIdAsync(Recepient recepients)
//        {
//            var order = Convert.ToInt32(recepients.Order) - 1;
//            return await Context.Recepients
//                .Where(x => x.Tempid == recepients.Tempid && x.Order == order)
//                .ToListAsync();
//        }

//        public async Task<IList<Recepient>> GetRecepientsLeft(string tempId)
//        {
//            return await Context.Recepients
//                .Where(x => x.Tempid == tempId && (bool)!x.Takenaction)
//                .OrderBy(x => x.Order)
//                .ToListAsync();
//        }

//        public async Task<IList<Recepient>> GetCurrentRecepientsList(string tempId)
//        {
//            var lastActionTaken = await Context.Recepients
//                .Where(x => x.Tempid == tempId && (bool)x.Takenaction)
//                .OrderByDescending(x => x.Order)
//                .FirstOrDefaultAsync();

//            if (lastActionTaken != null)
//            {
//                var nextOrder = lastActionTaken.Order + 1;
//                return await Context.Recepients
//                    .Where(x => x.Tempid == tempId && (bool)!x.Takenaction && x.Order == nextOrder)
//                    .OrderBy(x => x.Order)
//                    .ToListAsync();
//            }

//            return await Context.Recepients
//                .Where(x => x.Tempid == tempId && (bool)!x.Takenaction && x.Order == 1)
//                .OrderBy(x => x.Order)
//                .ToListAsync();
//        }

//        public async Task<Recepient> SaveReceipt(Recepient recepients)
//        {
//            await Context.Recepients.AddAsync(recepients);
//            await Context.SaveChangesAsync();
//            return recepients;
//        }

//        public async Task<IList<Recepient>> SaveRecepientsAsync(IList<Recepient> recepients)
//        {
//            await Context.Recepients.AddRangeAsync(recepients);
//            await Context.SaveChangesAsync();
//            return recepients;
//        }

//        public async Task DeleteRecepientsByTempId(IList<string> idList)
//        {
//            var recepientsToDelete = await Context.Recepients.Where(x => idList.Contains(x.Tempid)).ToListAsync();

//            if (recepientsToDelete != null && recepientsToDelete.Any())
//            {
//                Context.Recepients.RemoveRange(recepientsToDelete);
//                await Context.SaveChangesAsync();
//            }
//        }

//        public async Task<bool> UpdateRecepientsById(Recepient recepients)
//        {
//            var existingRecepients = await Context.Recepients.FirstOrDefaultAsync(rr => rr.Recepientid == recepients.Recepientid);
//            if (existingRecepients != null)
//            {
//                Context.Entry(existingRecepients).CurrentValues.SetValues(recepients);
//                await Context.SaveChangesAsync();
//                return true;
//            }
//            return false;
//        }

//        public async Task<bool> UpdateTakenActionOfRecepientById(string id)
//        {
//            var existingRecepients = await Context.Recepients.FirstOrDefaultAsync(uu => uu.Recepientid == id);
//            if (existingRecepients != null)
//            {
//                existingRecepients.Takenaction = true;
//                existingRecepients.Status = RecepientStatus.Signed;
//                await Context.SaveChangesAsync();
//                return true;
//            }
//            return false;
//        }

//        public async Task<Recepient?> FindRecepientsByCorelationId(string corelationId)
//        {
//            return await Context.Recepients
//                .FirstOrDefaultAsync(x => x.Correlationid == corelationId);
//        }

//        public async Task<Recepient?> DeclinedCommentDetailsAsync(string tempId)
//        {
//            return await Context.Recepients
//                .FirstOrDefaultAsync(x => x.Tempid == tempId && (bool)x.Decline);
//        }

//        public async Task<bool> DeclineSigningAsync(string tempId, string suid, string comment)
//        {
//            var recepient = await Context.Recepients
//                .FirstOrDefaultAsync(x => x.Tempid == tempId && x.Suid == suid);

//            if (recepient != null)
//            {
//                recepient.Declineremark = comment;
//                recepient.Decline = true;
//                recepient.Status = RecepientStatus.Rejected;

//                Context.Recepients.Update(recepient);
//                await Context.SaveChangesAsync();
//                return true;
//            }

//            return false;
//        }

//        public async Task<Recepient?> GetRecepientsBySuidAndTempId(string suid, string tempId)
//        {
//            var recepient = await Context.Recepients
//                .FirstOrDefaultAsync(x => x.Tempid == tempId && x.Suid == suid);

//            if (recepient == null)
//            {
//                recepient = await Context.Recepients
//                    .FirstOrDefaultAsync(x => x.Tempid == tempId);
//            }

//            return recepient;
//        }


//    }
//}
