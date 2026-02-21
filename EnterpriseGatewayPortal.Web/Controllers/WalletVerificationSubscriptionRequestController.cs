using DocumentFormat.OpenXml.Office2010.Excel;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Services;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.ViewModel;
using EnterpriseGatewayPortal.Web.ViewModel.VerifyWalletCredential;
using EnterpriseGatewayPortal.Web.ViewModel.Wallet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Configuration;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    public class WalletVerificationSubscriptionRequestController : BaseController
    {
        private readonly IWalletService _walletService;

        public WalletVerificationSubscriptionRequestController(IAdminLogService adminLogService, IWalletService walletService) : base(adminLogService)
        {
           
           _walletService = walletService;
        }
       
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var viewModel = new List<VerifyCredentialListViewModel>();

            var VerifiersList = await _walletService.GetAllCredentialsVerifieriesListByOrgId(OrganizationId);
            
            if (VerifiersList == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Get all wallet List", LogMessageType.FAILURE.ToString(), "Fail to get wallet list", UUID, Email);
                return NotFound();
            }
            else
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "Get all wallet List", LogMessageType.SUCCESS.ToString(), "Get wallet lists", UUID, Email);

                foreach (var item in VerifiersList)
                {
                    viewModel.Add(new VerifyCredentialListViewModel
                    {
                        id = item.id,
                        credentialName = item.credentialName,
                        credentialId = item.credentialId,
                        organizationName = item.organizationName,
                        emails = item.emails,
                        status = item.status,
                        createdDate = item.createdDate
                    });
                }
            }
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewVerifyCredentialRequestDetails(int id)
        {

            var credentialInDb = await _walletService.GetVerifyCredentialRequestById(id);
            if (credentialInDb == null)
            {
                SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential subcribe request details", LogMessageType.FAILURE.ToString(), "Fail to get Credential subscribe request details", UUID, Email);
                return NotFound();
            }


            var verifyCredentialViewModel = new VerifyCredentialDTO
            {
                id = credentialInDb.id,
                credentialId = credentialInDb.credentialId,
                credentialName = credentialInDb.credentialName,
                attributes = credentialInDb.attributes,
                emails = credentialInDb.emails,
                organizationId = OrganizationId,
                organizationName = credentialInDb.organizationName,
                createdDate = credentialInDb.createdDate,
                status = credentialInDb.status,
                configuration = credentialInDb.configuration,
                remarks = credentialInDb.remarks,

            };

            SendAdminLog(ModuleNameConstants.Wallet, ServiceNameConstants.WalletCredentials, "View Credential subcribe request details", LogMessageType.SUCCESS.ToString(), "Get Credential subscribe requestdetails of ", UUID, Email);

            return View(verifyCredentialViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            ApproveRejectWalletVerifierReqDTO dto = new ApproveRejectWalletVerifierReqDTO();
            dto.id = id;
            dto.remarks = null;

            var response = await _walletService.ActivateWalletVerifierSubscribeRequest(dto);
            if (response == null || !response.Success)
            {
                Alert alert = new Alert { IsSuccess = false, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return Json(new { success = false, message = response.Message });
            }

            Alert successAlert = new Alert { IsSuccess = true, Message = response.Message };
            TempData["Alert"] = JsonConvert.SerializeObject(successAlert);
            return Json(new { success = true, message = response.Message });
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id, string remarks)
        {
            ApproveRejectWalletVerifierReqDTO dto = new ApproveRejectWalletVerifierReqDTO();
            dto.id = id;
            dto.remarks = remarks;
            var response = await _walletService.RejectWalletVerifierSubscribeRequest(dto);

            if (response == null || !response.Success)
            {
                Alert alert = new Alert { IsSuccess = false, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return Json(new { success = false, message = response.Message });
            }
            else
            {
                Alert alert = new Alert { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return Json(new { success = true, message = response.Message });
            }

        }
    }
}
