
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Utilities;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.ViewModel.AttributeServiceTransactions;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    public class AttributeServiceTransactionsController : BaseController
    {
        private readonly IAttributeServiceTransactionsService _attributeServiceTransactionsService;
        public AttributeServiceTransactionsController(IAdminLogService adminLogService, IAttributeServiceTransactionsService attributeServiceTransactionsService) : base(adminLogService)
        {
            _attributeServiceTransactionsService= attributeServiceTransactionsService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var viewModel = new List<AttributeServiceViewModel>();
            var transactionsList = await _attributeServiceTransactionsService.GetTransactionsListAsync(organizationUid);
            transactionsList = transactionsList.Reverse().ToList();
            if (transactionsList == null)
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.AttributeServiceTransactions, "Get all Attribute Service Transactions List", LogMessageType.FAILURE.ToString(), "Fail to get Attribute Service Transactions List", UUID, Email);
                return NotFound();
            }
            else
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.AttributeServiceTransactions, "Get all Attribute Service Transactions List", LogMessageType.SUCCESS.ToString(), "Get Attribute Service Transactions list success", UUID, Email);

                foreach (var item in transactionsList)
                {

                    viewModel.Add(new AttributeServiceViewModel
                    {
                        Id = item.Id,
                        TransactionId = item.TransactionId,
                        ClientName = item.ClientName,
                        RequestDate = item.RequestDate,
                        Status = item.Status,
                        RequestProfile = item.RequestProfile

                    });
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails(int id)
        {

            var transactionsDetails = await _attributeServiceTransactionsService.GetTransactionsByIdAsync(id);
            if (transactionsDetails == null)
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.AttributeServiceTransactions, "Get all Attribute Service Transaction Details", LogMessageType.FAILURE.ToString(), "Fail to get Attribute Service Transactions Details", UUID, Email);
                return NotFound();
            }
            else
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.AttributeServiceTransactions, "Get all Attribute Service Transaction Details", LogMessageType.SUCCESS.ToString(), "Get Attribute Service Transactions Details success",UUID, Email);

                AttributeProfileRequest attributeProfileRequest = new AttributeProfileRequest
                {
                    Id = transactionsDetails.attributeProfileRequest.Id,
                    TransactionId = transactionsDetails.attributeProfileRequest.TransactionId,
                    ClientName = transactionsDetails.attributeProfileRequest.ClientName,
                    RequestDate = transactionsDetails.attributeProfileRequest.RequestDate,
                    RequestProfile = transactionsDetails.attributeProfileRequest.RequestProfile,
                    UserId = transactionsDetails.attributeProfileRequest.UserId

                };

                AttributeProfileConsent attributeProfileConsent = new AttributeProfileConsent
                {
                    ConsentStatus = transactionsDetails.attributeProfileConsent.ConsentStatus,
                    ApprovedProfileAttributes = transactionsDetails.attributeProfileConsent.ApprovedProfileAttributes,
                    RequestedProfileAttributes = transactionsDetails.attributeProfileConsent.RequestedProfileAttributes,
                    ConsentUpdatedDate = transactionsDetails.attributeProfileConsent.ConsentUpdatedDate


                };
                AttributeProfileStatus attributeProfileStatus = new AttributeProfileStatus
                {
                    Status = transactionsDetails.attributeProfileStatus.Status,
                    FailedReason = transactionsDetails.attributeProfileStatus.FailedReason,
                    DataPivotId = transactionsDetails.attributeProfileStatus.DataPivotId
                };

                AttributeServiceDetailsViewModel viewModel = new AttributeServiceDetailsViewModel
                {
                    attributeProfileRequest = attributeProfileRequest,
                    attributeProfileConsent = attributeProfileConsent,
                    attributeProfileStatus = attributeProfileStatus,

                };
                return PartialView("_TransactionDetails", viewModel);
            }

        }

    }
}
