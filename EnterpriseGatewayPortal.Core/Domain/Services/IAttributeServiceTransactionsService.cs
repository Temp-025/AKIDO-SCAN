using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IAttributeServiceTransactionsService
    {
        Task<IEnumerable<AttributeServiceTransactionListDTO>> GetTransactionsListAsync(string orgUid);

        Task<AttributeServiceTransactionsDTO> GetTransactionsByIdAsync(int id);
    }
}
