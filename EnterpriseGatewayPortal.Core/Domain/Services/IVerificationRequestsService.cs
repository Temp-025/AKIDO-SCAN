using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IVerificationRequestsService
    {
        Task<IEnumerable<VerificationDetailDTO>> GetVerificationRequestListAsync(string orgId, string email);

        Task<VerificationDetailDTO> GetDocverifyRequestDetailsByIdAsync(int docVerificationId);

        Task<ServiceResult> Download(string fileId);

        Task<ServiceResult> GetPreviewDocAsync(string id);

        Task<VerificationDetailDTO> GetDocverifyRequestDetailsByFileIdAsync(string fileid);

        Task<ServiceResult> SignVerificationRequestAsync(VerificationRequestTrueCopySignDTO document);

        Task<ServiceResult> StatusChangeToInProgress(int requestid);
    }
}
