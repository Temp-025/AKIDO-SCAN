using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Persistance.Repositories
{
    public class EncDecKeyRepository : GenericRepository<EncDecKey, EnterprisegatewayportalDbContext>,
            IEncDecKeyRepository
    {
        public EncDecKeyRepository(ILogger logger, EnterprisegatewayportalDbContext context) : base(context, logger)
        {
        }
    }
}
