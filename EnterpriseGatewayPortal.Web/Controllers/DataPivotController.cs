using DocumentFormat.OpenXml.Drawing.Charts;

using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Core.Services;
using EnterpriseGatewayPortal.Core.Utilities;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Models;
using EnterpriseGatewayPortal.Web.ViewModel;
using EnterpriseGatewayPortal.Web.ViewModel.DataPivot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using AlertViewModel = EnterpriseGatewayPortal.Web.Models.AlertViewModel;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    [Authorize]
    public class DataPivotController : BaseController
    {
        private readonly IDataPivotService _dataPivotService;
        private readonly IOrganizationService _organizationService;
        private readonly IScopeService _scopeService;
        
        public DataPivotController(IAdminLogService adminLogService, IDataPivotService dataPivotService, IOrganizationService organizationService, IScopeService scopeService) : base(adminLogService)
        {
            _dataPivotService = dataPivotService;
            _organizationService = organizationService;
            _scopeService = scopeService;
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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            var viewModel = new List<DataPivotViewModel>();
            var pivotList = await _dataPivotService.GetDataPivotListAsync(organizationUid);
            if (pivotList == null)
            {
                logMessage = $"Get Organization Data Pivots List";
                SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
                    "Failed to get Data Pivots list", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
            }
            else
            {
                logMessage = $"Get Organization Data Pivots List";
                SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
                    "Successfully received Data Pivots list", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

                foreach (var item in pivotList)
                {
                    var serviceConfigurationDeserialize = JsonConvert.DeserializeObject<ServiceConfiguration>(item.ServiceConfiguration);
                    viewModel.Add(new DataPivotViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        DisplayName = item.Description,
                        OrgnizationId = organizationUid,
                        AttributeConfiguration = item.AttributeConfiguration,
                        Serviceurl = serviceConfigurationDeserialize.Serviceurl,
                        AuthScheme = item.AuthScheme

                    });
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var scopeList = await _dataPivotService.GetProfileScopeList();
            var list1 = new List<SelectListItem>();
            foreach (var org in scopeList)
            {
                list1.Add(new SelectListItem { Text = org.Name, Value = org.Id.ToString() });
            }

            var authList = await _dataPivotService.GetAuthSchemesList();
            var list2 = new List<SelectListItem>();
            foreach (var auth in authList)
            {
                list2.Add(new SelectListItem { Text = auth.DisplayName, Value = auth.Name });
            }

            var viewModel = new DataPivotViewModel
            {

                ScopesList = list1,
                AuthSchemeList = list2
            };
            return View(viewModel);
            // return View();
        }

        public async Task<IActionResult> CreateData(DataPivotViewModel viewModel)
        {
            string logMessage;
            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);

            if (viewModel.Cert != null && viewModel.Cert.ContentType != "application/x-x509-ca-cert")
            {

                ModelState.AddModelError("Cert", "invalid signing certificate");
                return View("Create", viewModel);
            }
            ServiceConfiguration configuration = new ServiceConfiguration
            {
                Serviceurl = viewModel.Serviceurl,
                ClientId = viewModel.ClientId,
                ClientSecret = viewModel.ClientSecret

            };
            var serviceConfiguration = JsonConvert.SerializeObject(configuration).ToString();

            DataPivot dataPivot = new DataPivot
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.DisplayName,
                OrgnizationId = organizationUid,
                AttributeConfiguration = viewModel.AttributeConfiguration,
                PublicKeyCert = (viewModel.Cert != null ? getCertificate(viewModel.Cert) : ""),
                ServiceConfiguration = serviceConfiguration,
                CreatedBy = UUID,
                UpdatedBy = UUID,
                ScopeId = int.Parse(viewModel.Scopes),
                DataPivotUid = Guid.NewGuid().ToString(),
                AuthScheme = viewModel.AuthScheme,

            };
            var response = await _dataPivotService.CreatePivot(dataPivot);
            if (!response.Success)
            {
                logMessage = $"Get Organization Data Pivots List";
                SendAdminLog(ModuleNameConstants.DataPivots, ServiceNameConstants.DataPivots,
                    "Failed to added Data Pivot", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
                return NotFound();
            }
            else
            {

                logMessage = $"Get Organization Data Pivots List";
                SendAdminLog(ModuleNameConstants.DataPivots, ServiceNameConstants.DataPivots,
                    "Successfully to added Data Pivot", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

                return RedirectToAction("List");

            }

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var updateDataPivot = await _dataPivotService.GetDataPivotById(id);
            if (updateDataPivot == null)
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.DataPivots, "View DataPivot details", LogMessageType.FAILURE.ToString(), "Fail to get  details", UUID, Email);
                return NotFound();
            }
            var serviceConfigurationDeserialize = JsonConvert.DeserializeObject<ServiceConfiguration>(updateDataPivot.ServiceConfiguration);
            var orgList = await GetOrganizationList();
            //var scopesList = await GetScopesList();
            var scopeList = await _dataPivotService.GetProfileScopeList();
            var scopesList = scopeList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            var AuthList = await GetAuthSchemList();
           
            var viewModel = new DataPivotViewModel
            {

                Id = updateDataPivot.Id,
                Name = updateDataPivot.Name,
                DisplayName = updateDataPivot.Description,
                OrgnizationId = updateDataPivot.OrgnizationId,
                OrgName = OrganizationName,
                AttributeConfiguration = updateDataPivot.AttributeConfiguration,
                Serviceurl = serviceConfigurationDeserialize.Serviceurl,
                ClientId = serviceConfigurationDeserialize.ClientId,
                ClientSecret = serviceConfigurationDeserialize.ClientSecret,
                OrganizatioList = orgList,
                ScopesList = scopesList,
                Scopes = updateDataPivot.ScopeId.ToString(),
                AuthScheme = updateDataPivot.AuthScheme,
                AuthSchemeList = AuthList,
                CreatedBy = updateDataPivot.CreatedBy,
                UpdatedBy = updateDataPivot.UpdatedBy,
                IsFileUploaded = !String.IsNullOrEmpty(updateDataPivot.PublicKeyCert)
            };
            return View(viewModel);
        }

        [HttpPost]

        public async Task<IActionResult> Update(DataPivotViewModel viewModel)
        {

            var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
            if (viewModel.Cert != null && viewModel.Cert.ContentType != "application/x-x509-ca-cert")
            {

                var orgList = await GetOrganizationList();

                viewModel.OrganizatioList = orgList;

                ModelState.AddModelError("Cert", "invalid signing certificate");
                return View("Edit", viewModel);
            }
            var updateDataPivot = await _dataPivotService.GetDataPivotById(viewModel.Id);
            if (updateDataPivot == null)
            {
                var orgList = await GetOrganizationList();
                viewModel.OrganizatioList = orgList;

                return View("Edit", viewModel);
            }


            ServiceConfiguration configuration = new ServiceConfiguration
            {
                Serviceurl = viewModel.Serviceurl,
                ClientId = viewModel.ClientId,
                ClientSecret = viewModel.ClientSecret

            };
            var serviceConfiguration = JsonConvert.SerializeObject(configuration).ToString();
            updateDataPivot.Id = viewModel.Id;
            updateDataPivot.Name = viewModel.Name;
            updateDataPivot.Description = viewModel.DisplayName;
            updateDataPivot.OrgnizationId = organizationUid;
            updateDataPivot.AttributeConfiguration = viewModel.AttributeConfiguration;
            updateDataPivot.UpdatedBy = UUID;
            updateDataPivot.ServiceConfiguration = serviceConfiguration;
            updateDataPivot.AuthScheme = viewModel.AuthScheme;
            updateDataPivot.ScopeId = int.Parse(viewModel.Scopes);
            updateDataPivot.PublicKeyCert = (viewModel.Cert != null ? getCertificate(viewModel.Cert) : updateDataPivot.PublicKeyCert);

            var response = await _dataPivotService.UpdatePivotDataAsync(updateDataPivot);
            if (!response.Success)
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.DataPivots, "Update Data Pivot details", LogMessageType.FAILURE.ToString(), "Failed to Update Data Pivot", UUID, Email);

                return NotFound();
            }
            else
            {
                SendAdminLog(ModuleNameConstants.DigitalAuthentication, ServiceNameConstants.DataPivots, "Update Data Pivot details", LogMessageType.SUCCESS.ToString(), "Successfully Updated Data Pivot", UUID, Email);
                Alert alert = new Alert { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _dataPivotService.DeletePivotDataAsync(id);
                if (response != null && response.Success)
                {

                    AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    SendAdminLog(ModuleNameConstants.DataPivots, ServiceNameConstants.DataPivots, "Delete Data Pivot details", LogMessageType.SUCCESS.ToString(), "Delete  Data Pivot successfully", UUID, Email);
                    return new JsonResult(true);
                }
                else
                {
                    AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    SendAdminLog(ModuleNameConstants.DataPivots, ServiceNameConstants.DataPivots, "Delete Data Pivot details", LogMessageType.FAILURE.ToString(), "Failed to Delete  Data Pivot", UUID, Email);

                    return new JsonResult(false);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        async Task<List<SelectListItem>> GetScopesList()
        {

            var result = await _scopeService.GetScopeAttributeList();
            var list = new List<SelectListItem>();
            if (result == null)
            {
                return list;
            }
            else
            {
                foreach (var org in result)
                {

                    list.Add(new SelectListItem { Text = org.Name, Value = org.Id.ToString() });
                }

                return list;
            }

        }
        async Task<List<SelectListItem>> GetOrganizationList()
        {
            var result = await _organizationService.GetOrganizationNamesAndIdAysnc();
            var list = new List<SelectListItem>();
            if (result == null)
            {
                return list;
            }
            else
            {
                foreach (var org in result)
                {
                    var orgobj = org.Split(",");
                    list.Add(new SelectListItem { Text = orgobj[0], Value = orgobj[1] });
                }

                return list;
            }
        }
        async Task<List<SelectListItem>> GetAuthSchemList()
        {
            var result = await _dataPivotService.GetAuthSchemesList();
            var list = new List<SelectListItem>();
            
            if (result == null)
            {
                return list;
            }
            else
            {
                foreach (var auth in result)
                {
                   
                        list.Add(new SelectListItem { Text = auth.DisplayName, Value = auth.Name });
                    

                   
                }

                return list;
            }

           
            
        }
    }
}
