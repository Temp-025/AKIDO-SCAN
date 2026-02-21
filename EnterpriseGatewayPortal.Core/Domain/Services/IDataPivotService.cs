using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IDataPivotService
    {
        Task<IEnumerable<DataPivot>> GetDataPivotListAsync(string orgUid);

        Task<IEnumerable<Scope>> GetProfileScopeList();

        Task<IEnumerable<AuthScheme>> GetAuthSchemesList();
        Task<ServiceResult> CreatePivot(DataPivot dataPivot);

        Task<ServiceResult> UpdatePivotDataAsync(DataPivot dataPivot);

        Task<ServiceResult> DeletePivotDataAsync(int id);

        Task<DataPivot> GetDataPivotById(int id);

    }
}
