namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    //public class FileStorageRepository : GenericRepository<Filestorage, EnterprisegatewayportalDbContext>, IFileStorageRepository
    //{
    //    private readonly ILogger _logger;
    //    public FileStorageRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
    //    {
    //        _logger = logger;
    //    }

    //    public async Task<Filestorage?> GetFileStorageById(string id)
    //    {
    //        var file = await Context.Filestorages.AsNoTracking()
    //            .Where(x => x.Fileid == id).FirstOrDefaultAsync();

    //        return file;
    //    }

    //    public async Task<Filestorage> SaveFileStorage(Filestorage file)
    //    {
    //        Context.Filestorages.Add(file);
    //        await Context.SaveChangesAsync();
    //        return file;
    //    }

    //    public async Task DeleteFileStoragesByFileIdsAsync(IList<string> idList)
    //    {
    //        var documentsToDelete = await Context.Filestorages
    //            .Where(x => idList.Contains(x.Fileid))
    //            .ToListAsync();

    //        Context.Filestorages.RemoveRange(documentsToDelete);
    //        await Context.SaveChangesAsync();
    //    }

    //    public async Task DeleteFileStorageAsync(string id)
    //    {
    //        var documentToDelete = await Context.Filestorages
    //            .SingleOrDefaultAsync(x => x.Fileid == id);
    //        if (documentToDelete != null)
    //        {
    //            Context.Filestorages.RemoveRange(documentToDelete);
    //            await Context.SaveChangesAsync();
    //        }
    //    }

    //    public async Task<bool> UpdateFileStorageByFileId(Filestorage file)
    //    {
    //        var existingFileStorage = await Context.Filestorages
    //            .FirstOrDefaultAsync(x => x.Fileid == file.Fileid);

    //        if (existingFileStorage != null)
    //        {
    //            // Compare properties to check for modifications
    //            if (!existingFileStorage.File.SequenceEqual(file.File))
    //            {
    //                existingFileStorage.File = file.File;

    //                // Optional: Implement concurrency handling if needed
    //                // Context.Entry(existingFileStorage).OriginalValues.SetValues(file);

    //                await Context.SaveChangesAsync();

    //                return true; // Data was modified and saved
    //            }
    //        }

    //        return false;
    //    }
    //}
}
