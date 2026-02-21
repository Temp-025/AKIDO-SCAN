using EnterpriseGatewayPortal.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Repositories
{
    public interface IConfigurationRepository: IGenericRepository<Configuration>
    {
        Task<List<Configuration>> GetAll();
        //Task<Configuration> AddConfigurationAsync(Configuration model);
        Task AddConfigurationAsync(Configuration model);
    }
}
