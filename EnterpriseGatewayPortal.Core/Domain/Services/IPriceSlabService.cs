using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IPriceSlabService
    {
        Task<IEnumerable<PriceSlabDefinitionDTO>> GetAllPriceSlabDefinitionsAsync();

        Task<IEnumerable<OrgPriceSlabDTO>> GetAllOrgPriceSlabDefinitionsAsync();

        Task<IList<PriceSlabDefinitionDTO>> GetPriceSlabDefinitionAsync(int serviceId, string stakeholder);

        Task<IList<OrgPriceSlabDTO>> GetOrgPriceSlabDefinitionAsync(int serviceId, string organizationUid);

    }
}
