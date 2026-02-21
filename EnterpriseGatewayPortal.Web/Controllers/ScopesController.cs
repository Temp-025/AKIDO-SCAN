using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Models;
using EnterpriseGatewayPortal.Web.ViewModel.Scopes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    [Authorize]
    public class ScopesController : BaseController
    {
        private readonly IDataPivotService _dataPivotService;
        private readonly IScopeService _scopeService;
        public ScopesController(IAdminLogService adminLogService, IDataPivotService dataPivotService, IScopeService scopeService) : base(adminLogService)
        {
            _dataPivotService = dataPivotService;
            _scopeService = scopeService;

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var viewModel = new List<ScopesListViewModel>();

            var ScopeList = await _dataPivotService.GetProfileScopeList();
            if (ScopeList == null)
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Get all Scopes Configuration List", LogMessageType.FAILURE.ToString(), "Fail to get Scopes Configuration list", UUID, Email);
                return NotFound();
            }
            else
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Get all Scopes Configuration List", LogMessageType.SUCCESS.ToString(), "Get Scopes Configuration list success", UUID, Email);

                foreach (var item in ScopeList)
                {
                    viewModel.Add(new ScopesListViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        DisplayName = item.DisplayName,
                        Description = item.Description,
                        UserConsent = item.UserConsent
                    });
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var Claimslist = await GetClaimList(false);

            if (Claimslist == null)
            {
                return NotFound();
            }
            var viewModel = new ScopesNewViewModel
            {
                ClaimLists = Claimslist,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ScopesNewViewModel viewModel)
        {


            var scope = new Scope()
            {
                Name = viewModel.Name,
                DisplayName = viewModel.DisplayName,
                Description = viewModel.Description,
                UserConsent = viewModel.UserConsent,
                DefaultScope = viewModel.DefaultScope,
                MetadataPublish = viewModel.Metadata,
                CreatedBy = UUID,
                UpdatedBy = UUID,
                ClaimsList = viewModel.claims,
                IsClaimsPresent = viewModel.isClaimPresent
            };


            var response = await _scopeService.CreateScopeAsync(scope);
            if (response == null || !response.Success)
            {
                var Claimslist = await GetClaimList(false);
                if (Claimslist == null)
                {
                    return NotFound();
                }
                viewModel.ClaimLists = Claimslist;
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Create new Scopes Configuration", LogMessageType.FAILURE.ToString(), "Fail to create Scopes Configuration", UUID, Email);
                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return View("New", viewModel);
            }
            else
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Create new Scopes Configuration", LogMessageType.SUCCESS.ToString(), "Created New Scopes Configuration with name " + viewModel.DisplayName + " Successfully", UUID, Email);
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

                return RedirectToAction("List");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var scopeInDb = await _scopeService.GetScopeById(id);
            if (scopeInDb == null)
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "View Scopes Configuration client details", LogMessageType.FAILURE.ToString(), "Fail to get Scopes Configuration details", UUID, Email);
                return NotFound();
            }

            var Claimslist = await GetClaimList(scopeInDb.IsClaimsPresent, scopeInDb.ClaimsList);
            if (Claimslist == null)
            {
                return NotFound();
            }

            var ScopeEditViewModel = new ScopesEditViewModel
            {
                Id = scopeInDb.Id,
                Name = scopeInDb.Name,
                DisplayName = scopeInDb.DisplayName,
                Description = scopeInDb.Description,
                UserConsent = scopeInDb.UserConsent,
                DefaultScope = scopeInDb.DefaultScope,
                Metadata = scopeInDb.MetadataPublish,
                ClaimLists = Claimslist,
                claims = scopeInDb.ClaimsList,
                isClaimPresent = scopeInDb.IsClaimsPresent
            };

            SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "View Scopes Configuration details", LogMessageType.SUCCESS.ToString(), "Get Scopes Configuration details of " + scopeInDb.DisplayName + " successfully ", UUID, Email);

            return View(ScopeEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ScopesEditViewModel viewModel)
        {


            var scopeInDb = await _scopeService.GetScopeById(viewModel.Id);
            if (scopeInDb == null)
            {
                var Claimslist = await GetClaimList(viewModel.isClaimPresent, viewModel.claims);
                if (Claimslist == null)
                {
                    return NotFound();
                }
                viewModel.ClaimLists = Claimslist;
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Update Scopes Configuration", LogMessageType.FAILURE.ToString(), "Fail to get Scopes Configuration details", UUID, Email);
                ModelState.AddModelError(string.Empty, "Scopes Configuration not found");

                return View("Edit", viewModel);
            }


            scopeInDb.Id = viewModel.Id;
            scopeInDb.Name = viewModel.Name;
            scopeInDb.DisplayName = viewModel.DisplayName;
            scopeInDb.Description = viewModel.Description;
            scopeInDb.UserConsent = viewModel.UserConsent;
            scopeInDb.DefaultScope = viewModel.DefaultScope;
            scopeInDb.MetadataPublish = viewModel.Metadata;
            scopeInDb.UpdatedBy = UUID;
            scopeInDb.ClaimsList = viewModel.claims;
            scopeInDb.IsClaimsPresent = viewModel.isClaimPresent;



            var response = await _scopeService.UpdateScopeAsync(scopeInDb);
            if (response == null || !response.Success)
            {
                var Claimslist = await GetClaimList(viewModel.isClaimPresent, viewModel.claims);
                if (Claimslist == null)
                {
                    return NotFound();
                }
                viewModel.ClaimLists = Claimslist;
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Update Scopes Configuration", LogMessageType.FAILURE.ToString(), "Fail to update Scopes Configuration details of  name " + viewModel.DisplayName, UUID, Email);
                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return View("Edit", viewModel);
            }
            else
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Update Scopes Configuration", LogMessageType.SUCCESS.ToString(), (response.Message != "Your request sent for approval" ? "Updated Scopes Configuration details of  name " + viewModel.DisplayName + " successfully" : "Request for Update Scopes Configuration details of application name " + viewModel.DisplayName + " has send for approval "), UUID, Email);

                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _scopeService.DeleteScopeById(id);
                if (response != null || response.Success)
                {

                    AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Delete Scopes Configuration", LogMessageType.SUCCESS.ToString(), "Delete Scopes Configuration successfully", UUID, Email);
                    return new JsonResult(true);
                }
                else
                {
                    AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Delete Scopes Configuration", LogMessageType.FAILURE.ToString(), "Fail to delete Scopes Configuration", UUID, Email);
                    return new JsonResult(true);
                }
            }
            catch (Exception e)
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Delete Scopes Configuration", LogMessageType.FAILURE.ToString(), "Fail to delete Scopes Configuration " + e.Message, UUID, Email);
                return StatusCode(500, e);
            }
        }
        public async Task<List<ClaimListItem>> GetClaimList(bool isClaimPresent, string ScopeClaims = null)
        {
            var UserClaimList = await _scopeService.GetScopeAttributeList();
            if (UserClaimList == null)
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.ScopesConfiguration, "Get all user clamis list", LogMessageType.FAILURE.ToString(), "Fail to get all user clamis list", UUID, Email);
                throw new Exception("Fail to get user clamis");
            }
            var nodes = new List<ClaimListItem>();

            if (isClaimPresent && ScopeClaims != null)
            {
                var ScopeClaimslist = ScopeClaims.Split(" ");
                foreach (var claim in UserClaimList)
                {
                    nodes.Add(new ClaimListItem()
                    {
                        name = claim.Name,
                        Display = claim.DisplayName,
                        IsSelected = ScopeClaimslist.Any(x => x == claim.Name)
                    });
                }
            }
            else
            {
                foreach (var claim in UserClaimList)
                {

                    nodes.Add(new ClaimListItem()
                    {
                        name = claim.Name,
                        Display = claim.DisplayName,
                        IsSelected = false
                    });
                }
            }

            return nodes;
        }
    }
}
