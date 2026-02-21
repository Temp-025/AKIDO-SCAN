using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class MakerCheckerRepository : GenericRepository<MakerChecker, EnterprisegatewayportalDbContext>, IMakerCheckerRepository
    {
        private readonly ILogger _logger;
        public MakerCheckerRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<MakerChecker>> GetAllRequestsByMakerId(int id)
        {
            try
            {
                var list = await Context.MakerCheckers.
                    Where(t => t.MakerId == id).ToListAsync();
                return list.AsEnumerable().OrderByDescending(x => x.CreatedDate);
            }
            catch (Exception error)
            {
                _logger.LogError("GetAllRequestsByMakerId::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<MakerChecker>> GetAllRequestsByCheckerRoleId222(int id)
        {
            try
            {
                return await Context.MakerCheckers.
                    Where(t => t.ActivityId == id).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetAllRequestsByCheckerRoleId222::Database exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<MakerChecker>> GetAllRequestsByCheckerRoleId(int id)
        {
            //await Context.MakerCheckers.
            //    Where(t => t.ActivityId == t.Activity.RoleActivities.Where
            //    (x=>x.RoleId == id  && x.ActivityId == t.ActivityId)).ToListAsync();

            try
            {
                var result = new List<MakerChecker>()
                { };

                var roleActivities = await Context.RoleActivities.Where
                    (x => x.RoleId == id).ToListAsync();

                foreach (var item in roleActivities)
                {
                    var res = await Context.MakerCheckers.Where
                        (x => x.ActivityId == item.ActivityId && x.State == "PENDING")
                        .ToListAsync();
                    result = result.Concat(res).ToList();
                }

                return result.AsEnumerable().OrderByDescending(x => x.CreatedDate);
            }
            catch (Exception error)
            {
                _logger.LogError("GetAllRequestsByCheckerRoleId222::Database exception: {0}", error);
                return null;
            }
        }
    }
}
