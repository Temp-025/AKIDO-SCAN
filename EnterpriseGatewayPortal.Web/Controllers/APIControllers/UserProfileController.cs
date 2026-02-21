using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Web.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        [HttpGet]
        [Route("GetPidProfile/{userId}")]
        public async Task<IActionResult> GetUserPidProfile(string userId)
        {
            var response = await _userProfileService.GetPidProfile(userId);

            return Ok(new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            });
        }
        [HttpGet]
        [Route("GetMdlProfile/{userId}")]
        public async Task<IActionResult> GetUserMdlProfile(string userId)
        {
            var response = await _userProfileService.GetMdlProfile(userId);

            return Ok(new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            });
        }
        [HttpGet]
        [Route("GetSocialBenefitCard/{userId}")]
        public async Task<IActionResult> GetUserSocialBenefitProfile(string userId)
        {
            var response = await _userProfileService.GetSocialBenefitCardDetails(userId);

            return Ok(new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            });
        }

        [HttpGet]
        [Route("GetPOAProfile/{userId}")]
        public async Task<IActionResult> GetUserPOAProfile(string userId)
        {
            var response = await _userProfileService.GetPOAProfile(userId);

            return Ok(new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            });
        }

        [HttpGet]
        [Route("GetPOAProfileRequest/{userId}")]
        public async Task<IActionResult> GetUserPOAProfileRequest(string userId)
        {
            var response = await _userProfileService.GetPOAProfileRequest(userId);

            return Ok(new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            });
        }

        [HttpGet]
        [Route("GetUaeIdProfile/{userId}")]
        public async Task<IActionResult> GetUaeIdProfile(string userId)
        {
            var response = await _userProfileService.GetUaeidProfile(userId);

            return Ok(new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            });
        }

        [HttpGet]
        [Route("GetUaeidAuthProfile/{userId}")]
        public async Task<IActionResult> GetUaeIdProfileRequest(string userId)
        {
            var response = await _userProfileService.GetUaeidAuthProfile(userId);
            return Ok(new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            });
        }

        [HttpGet]
        [Route("GetEmiratesIdProfile/{userId}")]
        public async Task<IActionResult> GetEmiratesIdProfile(string userId)
        {
            var response = await _userProfileService.GetEmiratesIdProfile(userId);
            return Ok(new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            });
        }

        [HttpGet]
        [Route("GetPassportProfile/{userId}")]
        public async Task<IActionResult> GetPassportProfile(string userId)
        {
            var response = await _userProfileService.GetPassportProfile(userId);
            return Ok(new APIResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Result = response.Resource
            });
        }
    }
}
