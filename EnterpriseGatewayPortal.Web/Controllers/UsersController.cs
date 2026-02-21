using EnterpriseGatewayPortal.Core.Domain.Lookups;
using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Web.Constants;
using EnterpriseGatewayPortal.Web.Enums;
using EnterpriseGatewayPortal.Web.Models;
using EnterpriseGatewayPortal.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        public UsersController(IAdminLogService adminLogService, ILogger<UsersController> logger, IUserService userService) : base(adminLogService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userService.ListUsersAsync();
            if (users == null)
            {
                return NotFound();
            }

            else
            {
                var userlist = new List<Models.Users.User>();
                foreach (var obj in users)
                {
                    userlist.Add(new Models.Users.User
                    {
                        Id = obj.Id,
                        FullName = obj.Name,
                        MailId = obj.Email,
                        Role = obj.Role.Name,
                        Status = obj.Status
                    });
                }
                SendAdminLog(ModuleNameConstants.Users,
                 ServiceNameConstants.Users,
                 "Get all Users List",
                 LogMessageType.SUCCESS.ToString(),
                 "Get all Users list success", UUID, Email);
                return View(userlist);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetJson()
        {
            var totalRecords = 0;
            var users = await _userService.ListUsersAsync();
            if (users == null)
            {
                return NotFound();
            }
            if (users == null)
            {
                var displayResult = new List<Core.Domain.Models.User>();
                totalRecords = 0;
                return Json(new { recordsFiltered = totalRecords, recordsTotal = totalRecords, data = displayResult });
            }
            else
            {
                var userlist = new List<Models.Users.User>();
                foreach (var obj in users)
                {
                    userlist.Add(new Models.Users.User
                    {
                        Id = obj.Id,
                        FullName = obj.Name,
                        MailId = obj.Email,
                        Role = obj.Role.Name,
                        Status = obj.Status
                    });
                }
                var displayResult = userlist;
                totalRecords = userlist.Count();
                return Json(new { recordsFiltered = totalRecords, recordsTotal = totalRecords, data = displayResult });
            }
        }
        public async Task<IActionResult> New()
        {
            var roleLookups = GetRoleList(await _userService.GetRoleLookupsAsync());
            if (roleLookups == null)
            {
                return NotFound();
            }
            var userViewModel = new UserNewViewModel
            {
                Roles = roleLookups
            };

            return View(userViewModel);
        }
        public async Task<IActionResult> Save(UserNewViewModel viewModel)
        {
            if (viewModel.MailId != null)
            {
                viewModel.MailId = viewModel.MailId.Trim();
            }
            if (viewModel.FullName != null)
            {
                viewModel.FullName = viewModel.FullName.Trim();
            }
            if (!ModelState.IsValid)
            {
                viewModel.Roles = GetRoleList(await _userService.GetRoleLookupsAsync());
                return View("New", viewModel);
            }
            viewModel.MailId = viewModel.MailId.Trim();
            viewModel.FullName = viewModel.FullName.Trim();
            var email = viewModel.MailId;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,7})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                ModelState.AddModelError("MailId", "Invalid MailId");
                viewModel.Roles = GetRoleList(await _userService.GetRoleLookupsAsync());
                return View("New", viewModel);
            }

            viewModel.MailId = email;

            bool gender = true;
            if (viewModel.gender == 1) gender = true;
            else gender = false;

            if (viewModel.MobileNo.StartsWith("+91"))
            {
                ModelState.AddModelError("MobileNo", "Write only mobile number without country code");
                viewModel.Roles = GetRoleList(await _userService.GetRoleLookupsAsync());
                return View("New", viewModel);
            }

            var user = new Core.Domain.Models.User()
            {

                Name = viewModel.FullName,
                Email = viewModel.MailId,
                MobileNumber = "+91" + viewModel.MobileNo,
                RoleId = viewModel.RoleId,
                //RoleId = 1,
                Gender = gender,
                CreatedBy = UUID,
                ModifiedBy = UUID
            };

            var response = await _userService.AddUser(user);
            if (response == null || !response.Success)
            {
                SendAdminLog(ModuleNameConstants.Users, ServiceNameConstants.Users, "Create new User", LogMessageType.FAILURE.ToString(), "Failed to Create new User", UUID, Email);
                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                viewModel.Roles = GetRoleList(await _userService.GetRoleLookupsAsync());
                return View("New", viewModel);
            }
            else
            {
                SendAdminLog(ModuleNameConstants.Users, ServiceNameConstants.Users, "Create new User", LogMessageType.SUCCESS.ToString(), "Created user with name " + viewModel.FullName + " Successfully", UUID, Email);
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("List");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userInDb = await _userService.GetUserAsync(id);
            if (userInDb == null)
            {
                return NotFound();
            }
            var email = userInDb.Email;
            int gender = 0;
            if (userInDb.Gender == true)
            {
                gender = 1;
            }
            else
            {
                gender = 0;
            }
            var model = new UserEditViewModel
            {
                Id = userInDb.Id,
                Uuid = userInDb.Uuid,
                FullName = userInDb.Name,
                MailId = email,
                MobileNo = userInDb.MobileNumber.Replace("+91", ""),

                gender = gender,
                RoleId = userInDb.RoleId!.Value,

                Status = userInDb.Status,
                Roles = GetRoleList(await _userService.GetRoleLookupsAsync()),
            };
            SendAdminLog(ModuleNameConstants.Users, ServiceNameConstants.Users, "Get Details of User", LogMessageType.FAILURE.ToString(), "Get details of User Successfully", UUID, Email);
            return View(model);
        }

        public async Task<IActionResult> Update(UserEditViewModel viewModel)
        {
            if (viewModel.MailId != null)
            {
                viewModel.MailId = viewModel.MailId.Trim();
            }
            if (viewModel.FullName != null)
            {
                viewModel.FullName = viewModel.FullName.Trim();
            }

            if (!ModelState.IsValid)
            {
                viewModel.Roles = GetRoleList(await _userService.GetRoleLookupsAsync());
                return View("Edit", viewModel);
            }

            var email = viewModel.MailId;
            if (email == null)
            {
                ModelState.AddModelError("Email", "Please Provide Email");
                return View("Edit", viewModel);
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,7})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                ModelState.AddModelError("MailId", "Invalid MailId");
                viewModel.Roles = GetRoleList(await _userService.GetRoleLookupsAsync());
                return View("Edit", viewModel);
            }

            viewModel.MailId = email;


            if (viewModel.MobileNo.StartsWith("+91"))
            {
                ModelState.AddModelError("MobileNo", "Write only mobile number without country code");
                viewModel.Roles = GetRoleList(await _userService.GetRoleLookupsAsync());
                return View("Edit", viewModel);
            }

            var userInDb = await _userService.GetUserAsync(viewModel.Id);
            if (userInDb == null)
            {
                return NotFound();
            }
            bool gender = true;
            if (viewModel.gender == 0)
            {
                gender = false;
            }
            userInDb.Id = viewModel.Id;
            userInDb.Name = viewModel.FullName;
            userInDb.Email = viewModel.MailId;
            userInDb.MobileNumber = "+91" + viewModel.MobileNo;
            userInDb.RoleId = viewModel.RoleId;
            userInDb.Gender = gender;
            userInDb.ModifiedBy = UUID;

            var response = await _userService.UpdateUser(userInDb);
            if (response == null || !response.Success)
            {
                SendAdminLog(ModuleNameConstants.Users, ServiceNameConstants.Users, "Update user  details", LogMessageType.FAILURE.ToString(), "Fail to update user info of " + viewModel.FullName, UUID, Email);
                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                viewModel.Roles = GetRoleList(await _userService.GetRoleLookupsAsync());
                return RedirectToAction("Edit", new { id = viewModel.Id });
            }
            else
            {
                SendAdminLog(ModuleNameConstants.Users, ServiceNameConstants.Users, "Update user  details", LogMessageType.FAILURE.ToString(), "Updated user info of  " + viewModel.FullName + " successfully", UUID, Email);
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("List");
            }
        }
        public List<SelectListItem> GetRoleList(IEnumerable<RoleLookupItem> role)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (RoleLookupItem i in role)
            {
                list.Add(new SelectListItem { Value = i.Id.ToString(), Text = i.Name });
            }
            return list;
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(int id)
        {
            var Responce = await _userService.AdminResetPassword(id);
            if (Responce == null || !Responce.Success)
            {
                AlertViewModel alert = new AlertViewModel { Message = "Fail to reset user password" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
            }
            else
            {
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "New Password Sent To Mail Successfully" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

            }
            return RedirectToAction("Edit", "Users", new { id = id });
        }
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var Responce = await _userService.DeleteUser(Id);
            if (Responce == null || !Responce.Success)
            {
                AlertViewModel alert = new AlertViewModel { Message = "Failed to Delete user" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
            }
            else
            {
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "Successfully Deleted User" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

            }
            return RedirectToAction("List");
        }
        public async Task<IActionResult> ActivateUser(int Id)
        {
            var Responce = await _userService.ActivateUser(Id);
            if (Responce == null || !Responce.Success)
            {
                AlertViewModel alert = new AlertViewModel { Message = "Fail to reset user password" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
            }
            else
            {
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "New Password Sent To Mail Successfully" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

            }
            return RedirectToAction("Edit", "Users", new { id = Id });
        }
    }
}
