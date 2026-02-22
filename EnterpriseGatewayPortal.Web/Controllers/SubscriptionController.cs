using EnterpriseGatewayPortal.Core.Constants;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.ViewModel.SubscriptionVerification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    [Authorize]
    public class SubscriptionController : BaseController
    {

        private readonly IConfiguration _configuration;
        private readonly ISubscriptionVerifyService _subscriptionVerifyService;
        private readonly IDocumentVerifyIssuerService _documentVerifyIssuerService;
        public SubscriptionController(IAdminLogService adminLogService, IConfiguration configuration, ISubscriptionVerifyService subscriptionVerifyService, IDocumentVerifyIssuerService documentVerifyIssuerService) : base(adminLogService)
        {
            _configuration = configuration;
            _subscriptionVerifyService = subscriptionVerifyService;
            _documentVerifyIssuerService = documentVerifyIssuerService;
        }



        [HttpGet]
        public async Task<IActionResult> SubscriptionList()
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var orgid = organizationUid.ToString();
            var response = await _subscriptionVerifyService.GetAllSubscriptionListByIdAsync(orgid);
            var subscriptionVerifyDetailsEnumerable = (IEnumerable<SubscriptionVerifyListDTO>)response.Resource;


            SubscriptionVerificationListViewModel viewModel = new SubscriptionVerificationListViewModel()
            {
                SubscriptionVerifyLists = subscriptionVerifyDetailsEnumerable

            };

            logMessage = $"Successfully received Subscription Users List";
            SendAdminLog(ModuleNameConstants.Subscription, ServiceNameConstants.Subscription,
                "Get Subscription users details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewSubscriptionDetails(int id)
        {
            var response = await _subscriptionVerifyService.GetSubscriptionDetailByIdAsync(id);
            var subscriptionDetails = (SubscriptionVerifyListDTO)response.Resource;


            SubscriptionDetailsViewModel model = new SubscriptionDetailsViewModel
            {
                Id = subscriptionDetails.Id,
                DocumentIssuerName = string.IsNullOrEmpty(subscriptionDetails.DocumentIssuerName) ? "N/A" : subscriptionDetails.DocumentIssuerName,
                CreatedAt = subscriptionDetails.CreatedAt.HasValue ? subscriptionDetails.CreatedAt.ToString() : "N/A",
                UpdatedAt = subscriptionDetails.UpdatedAt.HasValue ? subscriptionDetails.UpdatedAt.ToString() : "N/A",
                SubscriptionDate = subscriptionDetails.SubscriptionDate.HasValue ? subscriptionDetails.SubscriptionDate.ToString() : "N/A",
                SubscriptionFee = string.IsNullOrEmpty(subscriptionDetails.SubscriptionFee) ? "N/A" : subscriptionDetails.SubscriptionFee,
                CreatedBy = string.IsNullOrEmpty(subscriptionDetails.CreatedBy) ? "N/A" : subscriptionDetails.CreatedBy,
                UpdatedBy = string.IsNullOrEmpty(subscriptionDetails.UpdatedBy) ? "N/A" : subscriptionDetails.UpdatedBy,
                DocumentTypes = string.IsNullOrEmpty(subscriptionDetails.DocumentTypes) ? "N/A" : subscriptionDetails.DocumentTypes,
                ExpiryDate = subscriptionDetails.ExpiryDate.HasValue ? subscriptionDetails.ExpiryDate.ToString() : "N/A",
                Status = string.IsNullOrEmpty(subscriptionDetails.Status) ? "N/A" : subscriptionDetails.Status,

            };
            return View(model);
        }
        public async Task<IActionResult> CreateSubscription()
        {

            var response = await _documentVerifyIssuerService.GetAllIssuerOrgNamesListAsync();
            var issuerOrgNamesEnumerable = (IEnumerable<IssuerOrgNamesDTO>)response.Resource;
            List<IssuerOrgNamesDTO> issuerOrgNamesdetails = issuerOrgNamesEnumerable.ToList();

            CreateSubcriptionViewModel viewModel = new CreateSubcriptionViewModel()
            {
                IssuerOrgNameDetails = issuerOrgNamesdetails,

            };

            return View(viewModel);
        }

        public async Task<IActionResult> EditSubscription(string issuerUid)
        {

            var response = await _documentVerifyIssuerService.GetAllIssuerOrgNamesListAsync();
            var issuerOrgNamesEnumerable = (IEnumerable<IssuerOrgNamesDTO>)response.Resource;
            List<IssuerOrgNamesDTO> issuerOrgNamesdetails = issuerOrgNamesEnumerable.ToList();

            var response2 = await _subscriptionVerifyService.GetAllSubscriptionByissuerIdAsync(issuerUid);
            var subscriptionDetails = (SubscriptionVerifyListDTO)response2.Resource;

            EditSubcriptionViewModel viewModel = new EditSubcriptionViewModel()
            {
                IssuerOrgNameDetails = issuerOrgNamesdetails,
                DocumentNames = subscriptionDetails.DocumentTypes,
                IssuerOrgName = subscriptionDetails.DocumentIssuerName
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CreateSubcriptionViewModel createSubscriptionViewModel)
        {
            try
            {
                string logMessage;
                var issuerOrgName = createSubscriptionViewModel.issuerName;

                var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
                var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
                var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);


                var subscriptionModelDTO = new SubscriptionModelDTO
                {
                    IssuerUid = createSubscriptionViewModel.issuerUid,
                    DocumentIssuerName = createSubscriptionViewModel.issuerName,
                    DocumentTypes = createSubscriptionViewModel.selectedDocuments,
                    CreatedBy = FullName,
                    UpdatedBy = FullName,
                    Status = DocumentStatusConstants.Subscribed,
                    SubscriptionDate = DateTime.Now,
                    SubscriptionFee = "2000",
                    ExpiryDate = DateTime.Now.AddDays(365)
                };
                var response = await _subscriptionVerifyService.SaveSubscriptionDetails(subscriptionModelDTO);
                if (response.Success)
                {
                    logMessage = $"Successfully Added Subscription Request";
                    SendAdminLog(ModuleNameConstants.Subscription, ServiceNameConstants.Subscription,
                        "Get Subscription Request", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                    return Json(new { success = true, response.Message });
                }
                else
                {
                    logMessage = $"Failed to Add the Subscription Request ";
                    SendAdminLog(ModuleNameConstants.Subscription, ServiceNameConstants.Subscription,
                        "Get Subscription Request", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                    return Json(new { success = false, response.Message });
                }


            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error processing the data." });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] EditSubcriptionViewModel editSubcriptionViewModel)
        {
            try
            {
                string logMessage;
                var issuerOrgName = editSubcriptionViewModel.issuerName;

                var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
                var user = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user").Value);
                var userDTO = JsonConvert.DeserializeObject<UserDTO>(user);
                var res = await _subscriptionVerifyService.GetAllSubscriptionByissuerIdAsync(editSubcriptionViewModel.issuerUid);
                var subscriptionDetails = (SubscriptionVerifyListDTO)res.Resource;

                var subscriptionUpdateDTO = new SubscriptionUpdateDTO
                {
                    Id = subscriptionDetails.Id,
                    IssuerUid = editSubcriptionViewModel.issuerUid,
                    DocumentIssuerName = editSubcriptionViewModel.issuerName,
                    DocumentTypes = editSubcriptionViewModel.selectedDocuments,
                    CreatedBy = FullName,
                    UpdatedBy = FullName,
                    Status = DocumentStatusConstants.Subscribed,
                    CreatedAt = subscriptionDetails.CreatedAt,
                    UpdatedAt = DateTime.Now,
                    SubscriptionDate = subscriptionDetails.SubscriptionDate,
                    SubscriptionFee = "2000",
                    ExpiryDate = subscriptionDetails.ExpiryDate
                };
                var response = await _subscriptionVerifyService.UpdateSubscriptionDetails(subscriptionUpdateDTO);
                if (response.Success)
                {
                    logMessage = $"Successfully Updated Subscription Request";
                    SendAdminLog(ModuleNameConstants.Subscription, ServiceNameConstants.Subscription,
                        "Get Subscription Request", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                    return Json(new { success = true, response.Message });
                }
                else
                {
                    logMessage = $"Failed to Update the Subscription Request ";
                    SendAdminLog(ModuleNameConstants.Subscription, ServiceNameConstants.Subscription,
                        "Get Subscription Request", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

                    return Json(new { success = false, response.Message });
                }


            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error processing the data." });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetDocumentType(string issuerUid)
        {
            var response = await _documentVerifyIssuerService.GetDocTypeListByIdAsync(issuerUid);

            var documentTypeEnumerable = (IEnumerable<IssuerOrgNamesDTO>)response.Resource;
            List<IssuerOrgNamesDTO> documentTypeDetails = documentTypeEnumerable.ToList();

            var response2 = await _subscriptionVerifyService.GetAllSubscriptionByissuerIdAsync(issuerUid);

            return Json(new { success = true, message = response.Message, result = documentTypeDetails });
        }

        public IActionResult SubscriptionFee(string selectedDocs)
        {

            List<string> docNames = selectedDocs.Split(',').ToList();
            int defaultSubscriptionFee = _configuration.GetValue<int>("SubscriptionFee"); ;

            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (string docName in docNames)
            {
                result.Add(docName, defaultSubscriptionFee);
            }

            return Json(new { success = true, message = "Subscription Fee details.", result });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckRecord(string issuerUid)
        {
            try
            {
                var response = await _subscriptionVerifyService.GetAllSubscriptionByissuerIdAsync(issuerUid);

                if (response.Success)
                {

                    return Json(new { success = true, message = response.Message });
                }
                else
                {
                    return Json(new { success = false, message = response.Message });
                }
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "An error occurred while processing the request." });
            }
        }



    }
}
