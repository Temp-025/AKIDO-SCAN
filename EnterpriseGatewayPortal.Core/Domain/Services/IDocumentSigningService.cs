using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IDocumentSigningService
    {
        Task<ServiceResult> GetDocumentDataMyListAsync(string token);
    }
}
