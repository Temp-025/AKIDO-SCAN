using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IQrCredentialService
    {
        Task<IEnumerable<QrCredentialDTO>> GetAllQRCredentialsList(string orgUid);
        Task<ServiceResult> AddQRCredentialsAsync(QrCredentialDTO QRCredentialAddDTO);

        Task<ServiceResult> QRTestCredentialByDocumentId(QrTestCredentialDTO qrTestCredentialDTO);

        Task<IEnumerable<QrCredentialVerifierDTO>> GetAllQRCredentialsListByOrgId(string orgID);

        Task<string[]> GetQRCredentialNamesAndIdListAysnc(string orgId);

        Task<QrCredentialDTO> GetQRCredentialById(string id);

        Task<ServiceResult> AddQRVerifyCredentialRequestAsync(QrCredentialVerifierDTO verifyCredential);

        Task<QrCredentialVerifierDTO> GetQRVerifyCredentialById(int id);

        Task<ServiceResult> UpdateVerifyQRCredentialRequestAsync(QrCredentialVerifierDTO verifyCredential);

        Task<IEnumerable<QrCredentialVerifierDTO>> GetAllCredentialsVerifieriesListByOrgId(string orgID);
        Task<QrCredentialVerifierDTO> GetVerifyCredentialRequestById(int id);

        Task<ServiceResult> ActivateWalletVerifierSubscribeRequest(ApproveRejectWalletVerifierReqDTO dto);

        Task<ServiceResult> RejectWalletVerifierSubscribeRequest(ApproveRejectWalletVerifierReqDTO dto);



    }
}
