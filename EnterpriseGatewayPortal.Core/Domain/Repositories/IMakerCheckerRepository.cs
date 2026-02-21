using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IMakerCheckerRepository : IGenericRepository<MakerChecker>
    {
        Task<IEnumerable<MakerChecker>> GetAllRequestsByMakerId(int id);
        Task<IEnumerable<MakerChecker>> GetAllRequestsByCheckerRoleId(int id);
    }
}
