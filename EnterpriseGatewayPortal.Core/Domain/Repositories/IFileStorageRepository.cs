using EnterpriseGatewayPortal.Core.Domain.Models;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IFileStorageRepository : IGenericRepository<Filestorage>
    {
        Task DeleteFileStorageAsync(string id);
        Task DeleteFileStoragesByFileIdsAsync(IList<string> idList);
        Task<Filestorage?> GetFileStorageById(string id);
        Task<Filestorage> SaveFileStorage(Filestorage file);
        Task<bool> UpdateFileStorageByFileId(Filestorage file);
    }
}
