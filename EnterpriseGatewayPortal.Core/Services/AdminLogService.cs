using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Utilities;
using Microsoft.Extensions.Logging;

namespace EnterpriseGatewayPortal.Core.Services
{
    public class AdminLogService : IAdminLogService
    {

        private readonly ILogger<AdminLogService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AdminLogService(IUnitOfWork unitOfWork, ILogger<AdminLogService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AdminLog>> GetLatestLogsAsync(int count)
        {
            if (count == 0)
            {
                _logger.LogError("count is Zero");
                return Enumerable.Empty<AdminLog>();
            }
            try
            {
                return await _unitOfWork.AdminLog.GetLatestLogsAsync(count);

            }
            catch (Exception ex)
            {
                _logger.LogError($"GetLatestLogsAsync Error: {ex.Message}");
                return Enumerable.Empty<AdminLog>();
            }


        }


        public async Task SaveAdminLogDetails(AdminLogDTO adminLogDTO)
        {
            if (adminLogDTO == null)
            {
                _logger.LogError("SaveAdminLogDetails: adminLogDTO is null.");
                throw new ArgumentNullException(nameof(adminLogDTO), "The AdminLogDTO object is null.");
            }

            try
            {
                var adminLog = new AdminLog
                {
                    Modulename = adminLogDTO.ModuleName,
                    Servicename = adminLogDTO.ServiceName,
                    Activityname = adminLogDTO.ActivityName,
                    Logmessagetype = adminLogDTO.LogMessageType,
                    Logmessage = adminLogDTO.LogMessage,
                    Datatransformation = adminLogDTO.DataTransformation,
                    Username = adminLogDTO.UserName,
                    Identifier = adminLogDTO.Identifier,
                    Identifieremail = adminLogDTO.IdentifierName,
                    Timestamp = DateTime.UtcNow,
                };

                await _unitOfWork.AdminLog.AddAdminLogAsync(adminLog);
            }
            catch (ArgumentNullException argEx)
            {
                // Log and rethrow argument-specific exceptions
                _logger.LogError(argEx, "SaveAdminLogDetails encountered a null argument.");
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception with detailed information
                _logger.LogError($"SaveAdminLogDetails Error: {ex.Message}");
                throw; // Optionally rethrow to handle it higher up the call stack
            }
        }

        public async Task<string[]> GetAllAdminLogNames(string request)
        {
            return await _unitOfWork.AdminLog.GetAllAdminLogUserNames(request);
        }
        public async Task<PaginatedList<AdminLogReportDTO>> GetLocalAdminLogReportsAsync(string startDate, string endDate, string userName = null,
  string moduleName = null, int page = 1, int perPage = 10)
        {
            try
            {
                _logger.LogInformation("GetLocalAdminLogReportsAsync Called");

                // Fetch logs from the database
                var logs = await _unitOfWork.AdminLog.GetLocalAdminLogReportByTimeStampAsync(startDate, endDate, userName, moduleName, page, perPage);

                if (logs == null || !logs.Any())
                {
                    _logger.LogError("Logs list is empty");
                    return new PaginatedList<AdminLogReportDTO>(Enumerable.Empty<AdminLogReportDTO>(), page, perPage, 0, 0);
                }

                // Map AdminLog to AdminLogReportDTO
                var logsDto = logs.Select(log => new AdminLogReportDTO
                {
                    _id = log.Id.ToString(),
                    ModuleName = log.Modulename,
                    ServiceName = log.Servicename,
                    ActivityName = log.Activityname,
                    Timestamp = log.Timestamp?.ToString("yyyy-MM-dd HH:mm:ss"), // Format the DateTime if not null
                    LogMessage = log.Logmessage,
                    LogMessageType = log.Logmessagetype,
                    UserName = log.Username,
                    DataTransformation = log.Datatransformation,
                    Checksum = log.Checksum,
                    IsChecksumValid = !string.IsNullOrEmpty(log.Checksum),
                    __v = 0 // Assign a default or actual value as required
                });

                var totalCount = await _unitOfWork.AdminLog.GetTotalLogCountAsync(startDate, endDate, userName, moduleName);
                var totalPages = (int)Math.Ceiling(totalCount / (double)perPage);

                // Return paginated list
                return new PaginatedList<AdminLogReportDTO>(logsDto, page, perPage, totalPages, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetLocalAdminLogReportsAsync Error: {ex.Message}");
                return new PaginatedList<AdminLogReportDTO>(Enumerable.Empty<AdminLogReportDTO>(), page, perPage, 0, 0);
            }
        }
    }
}
