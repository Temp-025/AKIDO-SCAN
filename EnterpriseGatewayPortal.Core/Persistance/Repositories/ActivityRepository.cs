using EnterpriseGatewayPortal.Core.Domain.Lookups;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class ActivityRepository : GenericRepository<Activity, EnterprisegatewayportalDbContext>, IActivityRespository
    {
        private readonly ILogger _logger;
        public ActivityRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<ActivityLookupItem>> GetActivityLookupItemsAsync()
        {
            try
            {
                return await Context.Activities.
                    Where(x => x.Enabled == true)
                    .Select(x =>
                    new ActivityLookupItem
                    {
                        Id = x.Id,
                        DisplayName = x.DisplayName,
                        McEnabled = x.McEnabled,
                        McSupported = x.McSupported,
                        ParentId = x.ParentId ?? 0
                    }).ToListAsync();
            }
            catch (Exception error)
            {
                _logger.LogError("GetActivityLookupItemsAsync::Database exception: {0}", error);
                return null;
            }
        }
    }
}

