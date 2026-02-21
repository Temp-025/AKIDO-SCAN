using EnterpriseGatewayPortal.Core.Domain.Services.Communication;

namespace EnterpriseGatewayPortal.Core.Domain.Services
{
    public interface IUserProfileService
    {
        public Task<ServiceResult> GetSocialBenefitCardDetails(string userId);
        public Task<ServiceResult> GetMdlProfile(string userId);
        public Task<ServiceResult> GetPidProfile(string userId);

        public Task<ServiceResult> GetPOAProfile(string userId);
        public Task<ServiceResult> GetPOAProfileRequest(string userId);
        public Task<ServiceResult> GetUaeidProfile(string userId);

        public Task<ServiceResult> GetEmiratesIdProfile(string userId);
        public Task<ServiceResult> GetPassportProfile(string userId);
        public Task<ServiceResult> GetUaeidAuthProfile(string userId);
    }
}