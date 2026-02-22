using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Models;
using EnterpriseGatewayPortal.Web.Models.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using ClientsEditViewModel = EnterpriseGatewayPortal.Core.DTOs.ClientsEditViewModel;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class ClientsController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly ILocalClientService _localClientService;
        private readonly IConfiguration _configuration;
        public ClientsController(IAdminLogService adminLogService,
            IClientService clientService,
            ILocalClientService localClientService,
            IConfiguration configuration) : base(adminLogService)
        {
            _clientService = clientService;
            _localClientService = localClientService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        string getCertificate(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }

            return result.ToString().Replace("\r", "");
        }
        public async Task<IActionResult> List()
        {
            //var response=await _clientService.getList();
            var response = await _localClientService.ListClientAsync();
            if (response == null)
            {
                return NotFound();
            }
            SendAdminLog(ModuleNameConstants.Applications,
                         ServiceNameConstants.Applications,
                         "Get all Applications List",
                         LogMessageType.SUCCESS.ToString(),
                         "Get all Applications list success", UUID, Email);

            return View(response);
        }

        [HttpGet]
        public IActionResult New()
        {

            var scope = _configuration.GetSection("DTIDP_Config:Scopes").Get<List<string>>();
            var grant = _configuration.GetSection("DTIDP_Config:Grants").Get<List<string>>();
            var clientViewModel = new ClientsNewViewModel
            {
                GrantTypesList = grant,
                ScopesList = scope,
                Scopes = "",
                GrantTypes = ""
            };
            clientViewModel.ApplcationTypeList.ToList();

            return View(clientViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(ClientsNewViewModel viewModel)
        {

            var orgUID = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            if (viewModel.Cert != null && viewModel.Cert.ContentType != "application/x-x509-ca-cert")
            {
                var scope = _configuration.GetSection("DTIDP_Config:Scopes").Get<List<string>>();
                var grant = _configuration.GetSection("DTIDP_Config:Grants").Get<List<string>>();

                viewModel.GrantTypesList = grant;
                viewModel.ScopesList = scope;
                viewModel.Scopes = viewModel.Scopes != null ? viewModel.Scopes : "";
                viewModel.GrantTypes = viewModel.GrantTypes != null ? viewModel.GrantTypes : "";

                ModelState.AddModelError("Cert", "invalid signing certificate");
                return View("New", viewModel);
            }

            var responce = "";
            if (viewModel.GrantTypes.Contains("authorization_code") || viewModel.GrantTypes.Contains("authorization_code_with_pkce"))
            {
                responce = "code";
            }
            if (viewModel.GrantTypes.Contains("implicit"))
            {
                responce = (responce == "") ? responce + " token" : "token";
            }
            var PublicKeyCert = (viewModel.Cert != null ? getCertificate(viewModel.Cert) : "");

            SaveClientDTO model = new SaveClientDTO();

            model.ApplicationName = viewModel.ApplicationName;
            model.ApplicationType = viewModel.ApplicationType;
            model.ApplicationUri = viewModel.ApplicationUri;
            model.GrantTypes = viewModel.GrantTypes;
            model.ScopesList = viewModel.ScopesList;
            model.GrantTypesList = viewModel.GrantTypesList;
            model.Scopes = viewModel.Scopes;
            model.GrantTypes = viewModel.GrantTypes;
            model.RedirectUri = viewModel.RedirectUri;
            model.LogoutUri = viewModel.LogoutUri;
            model.OrganizationId = orgUID;
            model.Base64Cert = Convert.ToBase64String(Encoding.UTF8.GetBytes(PublicKeyCert));
            model.AuthSchemaId = "0";

            var response = await _clientService.SaveClient(model);

            if (response == null || !response.Success)
            {
                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("New");
            }


            JObject jsonObject = JObject.FromObject(response.Resource);

            Client client = jsonObject.ToObject<Client>();

            var result = await _localClientService.CreateClientAsync(client!);
            if (result == null || !result.Success)
            {
                SendAdminLog(ModuleNameConstants.Applications, ServiceNameConstants.Applications, "Create new Application", LogMessageType.FAILURE.ToString(), "Fail to create Application", UUID, Email);
                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("New");
            }
            else
            {
                SendAdminLog(ModuleNameConstants.Applications, ServiceNameConstants.Applications, "Create new Applications", LogMessageType.SUCCESS.ToString(), "Created New Application with application name " + viewModel.ApplicationName + " Successfully", UUID, Email);

                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("List");
            }


        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            var scope = _configuration.GetSection("DTIDP_Config:Scopes").Get<List<string>>();
            var grant = _configuration.GetSection("DTIDP_Config:Grants").Get<List<string>>();
            //var clientInDb = await _clientService.GetClientAsync(id);
            var clientInDb = await _localClientService.GetClientByClientIdAsync(id);
            if (clientInDb == null)
            {
                return NotFound();
            }

            var clientsEditViewModel = new ClientsEditViewModel
            {
                Id = clientInDb.Id,
                ClientId = clientInDb.ClientId,
                ClientSecret = clientInDb.ClientSecret,
                ApplicationType = clientInDb.ApplicationType,
                ApplicationName = clientInDb.ApplicationName,
                ApplicationUri = clientInDb.ApplicationUrl,
                RedirectUri = clientInDb.RedirectUri,
                LogoutUri = clientInDb.LogoutUri,
                GrantTypes = clientInDb.GrantTypes,
                Scopes = clientInDb.Scopes,
                //WithPkce = (bool)clientInDb.WithPkce,
                GrantTypesList = grant,
                ScopesList = scope,
                State = clientInDb.Status,
                OrganizationId = clientInDb.OrganizationUid,
                IsFileUploaded = (clientInDb.PublicKeyCert != null),
            };
            clientsEditViewModel.ApplcationTypeList.ToList();

            SendAdminLog(ModuleNameConstants.Applications, ServiceNameConstants.Applications, "View Applications details", LogMessageType.SUCCESS.ToString(), "Get Applications details of " + clientInDb.ApplicationName + " successfully ", UUID, Email);

            return View(clientsEditViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ClientsEditViewModel viewModel)
        {
            if (viewModel.ApplicationName != null)
            {
                viewModel.ApplicationName = viewModel.ApplicationName.Trim();
            }
            //if (!ModelState.IsValid)
            //{
            //    var scope = _configuration.GetSection("DTIDP_Config:Scopes").Get<List<string>>();
            //    if (scope == null)
            //    {
            //        return NotFound();
            //    }
            //    var grant = _configuration.GetSection("DTIDP_Config:Grants").Get<List<string>>();
            //    if (grant == null)
            //    {
            //        return NotFound();
            //    }
            //    viewModel.GrantTypesList = grant;
            //    viewModel.ScopesList = scope;
            //    viewModel.Scopes = viewModel.Scopes != null ? viewModel.Scopes : "";
            //    viewModel.GrantTypes = viewModel.GrantTypes != null ? viewModel.GrantTypes : "";

            //    return View("Edit", viewModel);
            //}

            var orgUID = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            try
            {
                var scope = _configuration.GetSection("DTIDP_Config:Scopes").Get<List<string>>();
                var grant = _configuration.GetSection("DTIDP_Config:Grants").Get<List<string>>();

                if (viewModel.Cert != null && viewModel.Cert.ContentType != "application/x-x509-ca-cert")
                {
                    if (scope == null)
                    {
                        return NotFound();
                    }

                    if (grant == null)
                    {
                        return NotFound();
                    }


                    viewModel.GrantTypesList = grant;
                    viewModel.ScopesList = scope;
                    viewModel.Scopes = viewModel.Scopes != null ? viewModel.Scopes : "";
                    viewModel.GrantTypes = viewModel.GrantTypes != null ? viewModel.GrantTypes : "";
                    ModelState.AddModelError("Cert", "invalid signing certificate");
                    return View("Edit", viewModel);
                }

                UpdateClientDTO model = new UpdateClientDTO();

                model.ApplicationName = viewModel.ApplicationName;
                model.ApplicationType = viewModel.ApplicationType;
                model.Id = viewModel.Id;
                model.ApplicationUri = viewModel.ApplicationUri;
                model.GrantTypes = viewModel.GrantTypes;
                model.ScopesList = viewModel.ScopesList;
                model.GrantTypesList = viewModel.GrantTypesList;
                model.Scopes = viewModel.Scopes;
                model.GrantTypes = viewModel.GrantTypes;
                model.RedirectUri = viewModel.RedirectUri;
                model.LogoutUri = viewModel.LogoutUri;
                model.ClientId = viewModel.ClientId;
                model.ClientSecret = viewModel.ClientSecret;
                model.OrganizationId = orgUID;
                model.AuthSchemaId = "0";

                var PublicKeyCert = (viewModel.Cert != null ? getCertificate(viewModel.Cert) : "");
                if (!string.IsNullOrEmpty(PublicKeyCert))
                {
                    model.Base64Cert = Convert.ToBase64String(Encoding.UTF8.GetBytes(PublicKeyCert));
                }
                var response = await _clientService.UpdateClientAsync(model);
                if (response == null || !response.Success)
                {
                    AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return RedirectToAction("Edit", new { id = viewModel.ClientId });
                }

                JObject jsonObject = JObject.FromObject(response.Resource);

                Client client = jsonObject.ToObject<Client>();

                var result = await _localClientService.UpdateClientAsync(client!, null);
                if (result == null || !result.Success)
                {
                    SendAdminLog(ModuleNameConstants.Applications,
                         ServiceNameConstants.Applications,
                         "Update Application",
                         LogMessageType.FAILURE.ToString(),
                         "Save Application Success", UUID, Email);
                    AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return RedirectToAction("New");
                }
                else
                {
                    SendAdminLog(ModuleNameConstants.Applications,
                         ServiceNameConstants.Applications,
                         "Update Application",
                         LogMessageType.SUCCESS.ToString(),
                         "Update Application Success", UUID, Email);
                    AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return RedirectToAction("List");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("List");
            }
        }
        public async Task<IActionResult> Delete(string clientId)
        {
            var response = await _clientService.DeleteClientByClientId(clientId);
            if (response == null || !response.Success)
            {
                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                SendAdminLog(ModuleNameConstants.Applications, ServiceNameConstants.Applications, "Delete Application", LogMessageType.FAILURE.ToString(), "Failed to Delete Application", UUID, Email);
                return RedirectToAction("List");
            }
            var result1 = await _localClientService.DeleteClientByClientId(clientId);
            if (result1 == null || !result1.Success)
            {
                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                SendAdminLog(ModuleNameConstants.Applications, ServiceNameConstants.Applications, "Delete Application", LogMessageType.FAILURE.ToString(), "Failed to Delete Application", UUID, Email);
                return RedirectToAction("List");
            }
            AlertViewModel alert1 = new AlertViewModel { IsSuccess = true, Message = response.Message };
            TempData["Alert"] = JsonConvert.SerializeObject(alert1);
            SendAdminLog(ModuleNameConstants.Applications, ServiceNameConstants.Applications, "Delete Application", LogMessageType.SUCCESS.ToString(), "Delete Application with Id " + clientId + " Successfully", UUID, Email);
            return RedirectToAction("List");
        }

    }
}
