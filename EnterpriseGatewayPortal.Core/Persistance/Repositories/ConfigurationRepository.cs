using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class ConfigurationRepository: GenericRepository<Configuration, EnterprisegatewayportalDbContext>, IConfigurationRepository
    {
        private readonly ILogger _logger;
        public ConfigurationRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<List<Configuration>> GetAll()
        {
            try
            {
                var list= await Context.Configurations.AsNoTracking().ToListAsync();
                return list;
            }
            catch (Exception error)
            {
                _logger.LogError("ListOAuth2ClientAsync::Database exception: {0}", error);
                return null;
            }
        }
        //public async Task AddConfigurationAsync(Configuration model)
        //{
        //    try
        //    {
        //        await _unitOfWork.c;
        //        await _unitOfWork.SaveAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("DatabaseExceptionAddingUser" + ex.Message);
        //        response.Success = false;
        //        response.Message = "Failed to add user";
        //    }
        //}
        public async Task AddConfigurationAsync(Configuration model)
        {
            try
            {
                await AddAsync(model);
            }
            catch (Exception error)
            {
                Logger.LogError("AddUserAsync:: Database Exception: {0}", error);
            }
        }
    }
}
