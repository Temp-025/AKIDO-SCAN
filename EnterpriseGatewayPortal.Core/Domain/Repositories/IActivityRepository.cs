using EnterpriseGatewayPortal.Core.Domain.Lookups;
using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IActivityRespository : IGenericRepository<Activity>
    {
        Task<IEnumerable<ActivityLookupItem>> GetActivityLookupItemsAsync();
    }
}
