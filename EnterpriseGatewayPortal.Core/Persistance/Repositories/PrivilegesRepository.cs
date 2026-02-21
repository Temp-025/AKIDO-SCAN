using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class PrivilegesRepository : GenericRepository<Privilege, EnterprisegatewayportalDbContext>, IPrivilegesRepository
    {
        private readonly ILogger _logger;
        public PrivilegesRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<List<Privilege>> GetAllActivePrivilegesAsync()
        {
            try
            {
                return await Context.Set<Privilege>()
                    .Where(p => p.Status == "ACTIVE")
                    .ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError("GetAllActivePrivilegesAsync:: Database Exception: {0}", error);
                return null;
            }
        }


    }
}
