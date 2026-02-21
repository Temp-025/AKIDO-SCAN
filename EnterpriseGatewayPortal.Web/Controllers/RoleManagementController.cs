using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Web.Attribute;
using EnterpriseGatewayPortal.Web.Models;
using EnterpriseGatewayPortal.Web.ViewModel.RoleManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [ServiceFilter(typeof(SessionValidationAttribute))]
    public class RoleManagementController : BaseController
    {
        private readonly IRoleManagementService _roleActivityService;

        public RoleManagementController(IAdminLogService adminLogService, IRoleManagementService roleActivityService) : base(adminLogService)
        {
            _roleActivityService = roleActivityService;
        }



        [HttpGet]
        public async Task<IActionResult> List()
        {
            var roleLookupItems = await _roleActivityService.GetRoleLookupItemsAsync();
            if (roleLookupItems == null)
            {
                return NotFound();
            }

            var viewModel = new RoleManagementListViewModel()
            {
                RoleLookupItems = roleLookupItems
            };


            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            try
            {
                var activities = JsonConvert.DeserializeObject(await GetActivitiesList());
                if (activities == null)
                {
                    return NotFound();
                }
                var CheckerActivitie = await GetCkeckerList();
                if (CheckerActivitie == null)
                {
                    return NotFound();
                }
                var viewModel = new RoleManagementNewViewModel
                {
                    Activities = activities,
                    CheckerActivitie = CheckerActivitie
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                AlertViewModel alert = new AlertViewModel { Message = "Internal Error please contact to admin" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("List");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var roleInDb = await _roleActivityService.GetRoleAsync(id);
                if (roleInDb == null)
                {
                    return NotFound();
                }

                var viewModel = new RoleManagementNewViewModel
                {
                    Id = roleInDb.Id,
                    RoleName = roleInDb.Name,
                    DisplayName = roleInDb.DisplayName,
                    Description = roleInDb.Description,
                    CheckerActivitie = await GetCkeckerList(roleInDb.RoleActivities),
                    Activities = JsonConvert.DeserializeObject(await GetActivitiesList(roleInDb.RoleActivities)),
                    Status = roleInDb.Status
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                AlertViewModel alert = new AlertViewModel { Message = "Internal Error please contact to admin" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(RoleManagementNewViewModel viewModel)
        {
            try
            {

                var activityArry = viewModel.Activitie.Split(",");

                var selectedActivities = viewModel.Activitie.Split(",")
                    .Where(x => !x.StartsWith("IsChecker_"))
                   .Select(x => new { activityId = int.Parse(x), isChecker = activityArry.Contains("IsChecker_" + x) })
                   .ToDictionary(x => x.activityId, x => x.isChecker);

                var role = new Role
                {
                    Name = viewModel.RoleName,
                    DisplayName = viewModel.DisplayName,
                    Description = viewModel.Description,
                    CreatedBy = UUID,
                    UpdatedBy = UUID
                };

                var response = await _roleActivityService.AddRoleAsync(role, selectedActivities);
                if (response == null || !response.Success)
                {

                    AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);

                    var Activity = JsonConvert.DeserializeObject(await GetActivitiesList());
                    if (Activity == null)
                    {
                        return NotFound();
                    }
                    viewModel.Activities = Activity;

                    var CheckerActivitie = await GetCkeckerList();
                    if (CheckerActivitie == null)
                    {
                        return NotFound();
                    }

                    viewModel.CheckerActivitie = CheckerActivitie;
                    return View("New", viewModel);
                }
                else
                {


                    AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return RedirectToAction("List");
                }
            }
            catch (Exception)
            {
                AlertViewModel alert = new AlertViewModel { Message = "Internal Error please contact to admin" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleManagementNewViewModel viewModel)
        {
            try
            {

                var roleInDb = await _roleActivityService.GetRoleAsync(viewModel.Id);
                if (roleInDb == null)
                {
                    return NotFound();
                }

                roleInDb.Name = viewModel.RoleName;
                roleInDb.DisplayName = viewModel.DisplayName;
                roleInDb.Description = viewModel.Description;
                roleInDb.UpdatedBy = UUID;

                var activityArry = viewModel.Activitie.Split(",");

                var selectedActivities = viewModel.Activitie.Split(",")
                    .Where(x => !x.StartsWith("IsChecker_"))
                   .Select(x => new { activityId = int.Parse(x), isChecker = activityArry.Contains("IsChecker_" + x) })
                   .ToDictionary(x => x.activityId, x => x.isChecker);

                var response = await _roleActivityService.UpdateRoleAsync(roleInDb, selectedActivities);
                if (response == null || !response.Success)
                {

                    AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    var Activity = JsonConvert.DeserializeObject(await GetActivitiesList());
                    if (Activity == null)
                    {
                        return NotFound();
                    }
                    viewModel.Activities = Activity;

                    var CheckerActivitie = await GetCkeckerList();
                    if (CheckerActivitie == null)
                    {
                        return NotFound();
                    }
                    viewModel.CheckerActivitie = CheckerActivitie;
                    return View("Edit", viewModel);
                }
                else
                {

                    AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);

                    return RedirectToAction("List");
                }
            }
            catch (Exception)
            {
                AlertViewModel alert = new AlertViewModel { Message = "Internal Error please contact to admin" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _roleActivityService.DeleteRoleAsync(id, UUID, false);
                if (response != null && response.Success)
                {
                    AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return new JsonResult(true);
                }
                else
                {
                    AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return null;
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        public async Task<ActionResult> SetRoleState(int id, string Doaction)
        {
            RoleResponse responce;
            if (Doaction == "Activation")
            {
                responce = await _roleActivityService.ActivateRoleAsync(id);
            }
            else
            {
                responce = await _roleActivityService.DeActivateRoleAsync(id);
            }
            if (responce == null || !responce.Success)
            {
                AlertViewModel alert = new AlertViewModel { Message = "Roles " + Doaction + " Fail" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
            }
            else
            {
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "Role " + Doaction + " Successfully" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
            }

            return RedirectToAction("Edit", new { id = id });
        }

        public async Task<string> GetActivitiesList(IEnumerable<RoleActivity> roleActivities = null)
        {
            var activityLookupItems = await _roleActivityService.GetActivityLookupItemsAsync();
            if (activityLookupItems == null)
            {
                throw new Exception("Fail to get role activity");
            }
            var nodes = new List<ActivityTreeItem>();

            if (roleActivities != null)
            {
                foreach (var activity in activityLookupItems)
                {
                    nodes.Add(new ActivityTreeItem()
                    {
                        id = activity.Id.ToString(),
                        parent = (activity.ParentId == 0 ? "#" : activity.ParentId.ToString()),
                        text = activity.DisplayName,
                        state = JsonConvert.DeserializeObject(GetState(roleActivities, activity.Id))
                    });
                }
            }
            else
            {
                foreach (var activity in activityLookupItems)
                {
                    nodes.Add(new ActivityTreeItem()
                    {
                        id = activity.Id.ToString(),
                        parent = (activity.ParentId == 0 ? "#" : activity.ParentId.ToString()),
                        text = activity.DisplayName,
                        state = JsonConvert.DeserializeObject(GetState(null, -1))
                    });
                }
            }

            var data = new JsonResult(nodes);
            return JsonConvert.SerializeObject(data.Value);
        }

        public async Task<List<CheckerListItem>> GetCkeckerList(IEnumerable<RoleActivity> roleActivities = null)
        {
            var activityLookupItems = await _roleActivityService.GetActivityLookupItemsAsync();
            if (activityLookupItems == null)
            {
                throw new Exception("Fail to get role activity");
            }
            var nodes = new List<CheckerListItem>();

            if (roleActivities != null)
            {
                foreach (var activity in activityLookupItems)
                {
                    if (activity.McSupported)
                    {
                        nodes.Add(new CheckerListItem()
                        {
                            id = "IsChecker_" + activity.Id.ToString(),
                            Display = activity.DisplayName + " Checker",
                            IsSelected = GetCheckerState(roleActivities, activity.Id)
                        }); ;
                    }
                }
            }
            else
            {
                foreach (var activity in activityLookupItems)
                {
                    if (activity.McSupported)
                    {
                        nodes.Add(new CheckerListItem()
                        {
                            id = "IsChecker_" + activity.Id.ToString(),
                            Display = activity.DisplayName + " Checker",
                            IsSelected = GetCheckerState(roleActivities!, activity.Id)
                        });
                    }
                }
            }

            return nodes;
        }

        [NonAction]
        private string GetState(IEnumerable<RoleActivity> roleActivity = null, int parentId = -1)
        {
            JsonResult data;
            if (roleActivity != null)
            {
                data = Json(new { selected = roleActivity.Any(x => x.ActivityId == parentId && (bool)x.IsEnabled!) });
            }
            else
            {
                data = Json(new { selected = false });
            }
            return JsonConvert.SerializeObject(data.Value);
        }

        [NonAction]
        private bool GetCheckerState(IEnumerable<RoleActivity> roleActivity = null, int parentId = -1)
        {
            bool data;
            if (roleActivity != null)
                data = roleActivity.Any(x => x.ActivityId == parentId && x.IsChecker);
            else
                data = false;

            return data;
        }


    }
}
