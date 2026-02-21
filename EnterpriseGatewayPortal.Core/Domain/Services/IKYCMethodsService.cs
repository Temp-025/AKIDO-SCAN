using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IKYCMethodsService
    {
        public Task<IEnumerable<VerificationMethodDTO>> GetKycMethodsListAysnc(string orgUid);
        public Task<APIResponse> RequestKYCMethodAsync(string orgUid, string methodUid);
        public Task<APIResponse> GetVerificationMethodsStatsAysnc(string orgUid);
        public Task<VerificationMethodStatusDTO> GetOrganizationVerificationMethodByUidAsync(string orgUid, string methodUid);
        public Task<IEnumerable<VerificationMethodDTO>> GetOrganizationDefaultMethods(string orgUid);
        public Task<IEnumerable<VerificationMethodDTO>> GetAllOrganizationRequestedMethods(string orgUid);
        public Task<List<VerificationMethodDTO>> GetPendingVerificationMethods(string orgUid);
    }
}