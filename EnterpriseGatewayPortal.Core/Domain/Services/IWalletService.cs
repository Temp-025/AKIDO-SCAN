using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IWalletService
    {
        //Task<IEnumerable<CredentialListDTO>> GetAllCredentialsList();
        Task<IEnumerable<CredentialListDTO>> GetAllCredentialsList(string orgUid);

        Task<string[]> GetCategoryNamesAndIdAysnc();

        Task<ServiceResult> AddCredentialsAsync(CredentialListDTO CredentialAddDTO);

        Task<CredentialListDTO> GetCredentialById(string id);

        Task<ServiceResult> TestCredentialByDocumentId(TestCredentialRequestDTO testCredentialRequestDTO);

        Task<IEnumerable<WalletConfigurationDTO>> GetWalletConfiguration(string credentialUId);

        Task<string[]> GetCredentialNamesAndIdAysnc(string orgId);

        Task<Dictionary<string, string>> GetAuthSchemeTypesAysnc();
        //Task<string[]> GetAuthSchemeTypesAysnc();
        Task<ServiceResult> RevokeredentialsAsync(RevokeCredentialDTO revokeCredentialDTO);

        Task<ServiceResult> AddVerifyCredentialRequestAsync(VerifyCredentialDTO verifyCredential);
        Task<ServiceResult> UpdateVerifyCredentialRequestAsync(VerifyCredentialDTO verifyCredential);
        Task<DefaultDataAttrbutesDTO> GetAttrbutesList();
        Task<DefaultDataAttrbutesDTO> GetPidAttrbutesList();
        Task<DefaultDataAttrbutesDTO> GetMdlAttrbutesList();
        Task<DefaultDataAttrbutesDTO> GetSocialBenefitAttrbutesList();
        Task<IEnumerable<VerifyCredentialDTO>> GetAllCredentialsListByOrgId(string orgID);

        Task<string[]> GetCredentialNamesAndIdListAysnc(string orgId);

        Task<VerifyCredentialDTO> GetVerifyCredentialById(int id);
        Task<ServiceResult> SendToApprovalRequest(ApprovalRequestDTO dto);
        //Task<ServiceResult> SendSigningRequestAsync(SigningRequestDTO signingRequest, UserDTO userDTO, string accessToken);
        Task<ServiceResult> UpdateCredentialsAsync(CredentialListDTO credentialListDTO);
        Task<IEnumerable<VerifyCredentialDTO>> GetAllCredentialsVerifieriesListByOrgId(string orgID);
        Task<VerifyCredentialDTO> GetVerifyCredentialRequestById(int id);

        Task<ServiceResult> ActivateWalletVerifierSubscribeRequest(ApproveRejectWalletVerifierReqDTO dto);

        Task<ServiceResult> RejectWalletVerifierSubscribeRequest(ApproveRejectWalletVerifierReqDTO dto);

        Task<IEnumerable<WalletDomainDTO>> GetCredentialConsentDetails();
        Task<List<OrganizationCategoryDTO>> GetOrgCategoriesList();

    }

}
