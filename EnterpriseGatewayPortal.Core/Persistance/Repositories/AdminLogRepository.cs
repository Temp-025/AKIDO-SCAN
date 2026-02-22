using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{


    public class AdminLogRepository : GenericRepository<AdminLog, EnterprisegatewayportalDbContext>, IAdminLogRepository
    {
        private readonly ILogger _logger;
        public AdminLogRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<AdminLog?> AddAdminLogAsync(AdminLog adminLog)
        {
            try
            {
                var adminlog = await Context.Set<AdminLog>().AddAsync(adminLog);
                var addedAdminLogs = adminlog.Entity;
                Context.SaveChanges();
                return addedAdminLogs;
            }
            catch (Exception error)
            {
                Logger.LogError("AddAdminLogAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<IEnumerable<AdminLog>> GetLatestLogsAsync(int count)
        {
            try
            {
                return await Context.AdminLogs
                            .OrderByDescending(log => log.Timestamp)
                            .Take(count)
                            .ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetLatestLogsAsync:: Database Exception: {0}", error);
                return null;
            }

        }

        public async Task<List<AdminLog>> GetLocalAdminLogReportByTimeStampAsync(string startDate, string endDate, string userName, string moduleName, int page, int perPage)
        {
            try
            {
                var startDateTime = DateTime.Parse(startDate).ToUniversalTime();
                var endDateTime = DateTime.Parse(endDate).ToUniversalTime();

                return await Context
                    .AdminLogs
                    .Where(log => log.Timestamp.HasValue &&
                                  log.Timestamp >= startDateTime &&
                                  log.Timestamp <= endDateTime &&
                                  (string.IsNullOrEmpty(userName) || log.Username == userName) &&
                                  (string.IsNullOrEmpty(moduleName) || log.Modulename == moduleName))
                    // .OrderBy(log => log.Id)
                    .OrderByDescending(u => u.Timestamp)
                    .Skip((page - 1) * perPage)
                    .Take(perPage)
                    .ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetLocalAdminLogReportByTimeStampAsync:: Database Exception: {0}", error);
                return null;
            }
        }

        public async Task<int> GetTotalLogCountAsync(string startDate, string endDate, string userName, string moduleName)
        {
            try
            {
                var startDateTime = DateTime.Parse(startDate).ToUniversalTime();
                var endDateTime = DateTime.Parse(endDate).ToUniversalTime();

                // Build the query
                var query = Context.AdminLogs.AsQueryable();

                query = query.Where(log =>
                            log.Timestamp.HasValue &&
                            log.Timestamp >= startDateTime &&
                            log.Timestamp <= endDateTime);

                if (!string.IsNullOrEmpty(userName))
                {
                    query = query.Where(log => log.Username == userName);
                }

                if (!string.IsNullOrEmpty(moduleName))
                {
                    query = query.Where(log => log.Modulename == moduleName);
                }
                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError($"GetTotalLogCountAsync Error: {ex.Message}");
                return 0;
            }
        }

        public async Task<string[]> GetAllAdminLogUserNames(string request)
        {
            try
            {
                var adminLogs = await Context.AdminLogs
                    .Where(x => request != null && x.Username.Contains(request))
                    .ToListAsync(); // Fetch data into memory

                var result = adminLogs
                    .GroupBy(x => x.Username) // Group by UserName
                 .SelectMany(group =>
                 {
                     var distinctUUIDs = group.Select(r => r.Identifier).Distinct().ToList();

                     if (distinctUUIDs.Count == 1)
                     {
                         // All UUIDs are the same, take the latest record
                         return group
                                .OrderByDescending(r => r.Timestamp) // Adjust this to your date field
                             .Take(1)
                             .Select(r => $"{r.Username}({r.Identifieremail})");
                     }
                     else
                     {
                         return group
                             .Select(r => $"{r.Username}({r.Identifieremail})")
                             .Distinct();
                     }
                 })
                 .ToArray();

                return result;
            }
            catch (Exception error)
            {
                _logger.LogError("GetAllAdminLogUserNames::Database exception: {0}", error);
                return null;
            }

        }

    }
}
