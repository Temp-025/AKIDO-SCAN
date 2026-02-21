using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IOrganizationService
    {
        Task<string[]> GetOrganizationNamesAysnc(string value);

        Task<string[]> GetOrganizationNamesAndIdAysnc();

        Task<string[]> GetActiveSubscribersEmailListAsync(string value);

        Task<IList<SignatureTemplatesDTO>> GetSignatureTemplateListAsyn();

        Task<OrganizationDTO> GetOrganizationDetailsAsync(string organizationName);

        Task<ServiceResult> GetOrganizationDetailsByUIdAsync(string organizationUid);

        Task<ServiceResult> AddOrganizationAsync(OrganizationDTO onboardingOrganization, bool makerCheckerFlag = false);

        Task<ServiceResult> UpdateOrganizationAsync(OrganizationDTO updateOrganization, bool makerCheckerFlag = false);

        Task<ServiceResult> DelinkOrganizationEmployeeAsync(DelinkOrganizationEmployeeDTO delinkOrganizationEmployee);

        Task<ServiceResult> ValidateEmailListAsync(List<string> value);

        Task<ServiceResult> IssueCertificateAsync(string organizationUid, string uuid, string transactionReferenceId, bool makerCheckerFlag = false);

        Task<ServiceResult> RevokeCertificateAsync(string organizationUid, int reasonId, string remarks, string uuid, bool makerCheckerFlag = false);

        Task<bool> IsOrganizationExists(string orgName);

        Task<ServiceResult> VerifyDocumentSignatureAsync(string organizationUid, string uuid, string docType, string signedDoc, IList<string> signatories);

        Task<ServiceResult> GetEsealCertificateStatus(string organizationUid);
        Task<ServiceResult> GetStakeholdersAsync(string organizationUid);

        Task<ServiceResult> AddStakeHolder(IList<StakeholderDTO> stakeholderDTO);
        Task<OrganizationDTO> GetOrganizationDetailsByUId(string organizationUid);

        Task<ServiceResult> UpdateAgentUrlAsync(AgentUrlAndSpocUpdateDTO agentUrlAndSpoc);
        Task<ServiceResult> UpdateSpocEmailAsync(AgentUrlAndSpocUpdateDTO agentUrlAndSpoc);

        Task<ServiceResult> UpdateEmailDomainAsync(EmailDomainUpdateDTO emailDomain);

        Task<ServiceResult> GetOrganizationCertificateDetailstAsync(string orgId);

        Task<OrganizationPrevilagesDTO> GetPrevilagesAsync(string organizationUid);


    }
}
