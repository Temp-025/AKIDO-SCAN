using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.Domain.Services.Communication;
using EnterpriseGatewayPortal.Web.Models;
using EnterpriseGatewayPortal.Web.Models.SecurityQuestions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    [Authorize]
    public class UserDashboardController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ISecurityQuestionService _securityQuestionService;
        private readonly ILogger<UserDashboardController> _logger;
        private readonly IUserPasswordService _userPasswordService;
        public UserDashboardController(IAdminLogService adminLogService,
            ISecurityQuestionService securityQuestionService,
            ILogger<UserDashboardController> logger,
            IUserService userService,
            IUserPasswordService userPasswordService) : base(adminLogService)
        {
            _securityQuestionService = securityQuestionService;
            _logger = logger;
            _userService = userService;
            _userPasswordService = userPasswordService;
        }
        public async Task<IActionResult> Profile()
        {
            var id = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value);
            var userInDb = await _userService.GetUserAsync(id);
            if (userInDb == null)
            {
                return NotFound();
            }

            var roleLookups = await _userService.GetRoleLookupsAsync();
            if (roleLookups == null)
            {
                return NotFound();
            }

            var email = userInDb.Email;

            var model = new ProfileViewModel
            {
                Id = userInDb.Id,

                FullName = userInDb.Name,
                MailId = email,
                gender = (userInDb.Gender ? 1 : 0),
                MobileNo = userInDb.MobileNumber.Replace("+91", ""),
                Role = roleLookups.FirstOrDefault(x => x.Id == userInDb.RoleId!.Value)?.Name,
                Status = userInDb.Status
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel viewModel)
        {

            var email = viewModel.MailId;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,7})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                ModelState.AddModelError("MailId", "Invalid MailId");
                return View("Profile", viewModel);
            }

            viewModel.MailId = email;


            if (viewModel.MobileNo.StartsWith("+91"))
            {
                ModelState.AddModelError("MobileNo", "Write only mobile number without country code");
                return View("Edit", viewModel);
            }

            var userInDb = await _userService.GetUserAsync(viewModel.Id);
            if (userInDb == null)
            {
                return NotFound();
            }

            userInDb.Id = viewModel.Id;
            userInDb.Name = viewModel.FullName;
            userInDb.Email = viewModel.MailId;
            userInDb.Gender = (viewModel.gender == 1) ? true : false;
            userInDb.MobileNumber = "+91" + viewModel.MobileNo;

            userInDb.ModifiedBy = UUID;


            var response = await _userService.UpdateUser(userInDb);
            if (response == null || !response.Success)
            {

                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return View("Profile", viewModel);
            }
            else
            {
                AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "Profile Updated Successfully" };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);

                return RedirectToAction("Profile");
            }
        }
        public IActionResult SetSecurityQuestion()
        {
            var id = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value);
            var model = new SecurityQuestionViewModel()
            {
                UserId = id,
                SecurityQueList1 = getSecurityQuestionList(1),
                SecurityQueList2 = getSecurityQuestionList(2)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetSecurityQuestion(SecurityQuestionViewModel viewModel)
        {
            try
            {

                var QueAns1 = new UserSecurityQue
                {
                    UserId = viewModel.UserId,
                    Question = viewModel.Question1,
                    Answer = viewModel.Answer1,
                    UpdatedBy = UUID,
                    CreatedBy = UUID,
                };
                var response = await _securityQuestionService.CreateUserSecurityQnsAns(QueAns1);
                if (response == null || !response.Success)
                {
                    viewModel.SecurityQueList1 = getSecurityQuestionList(1);
                    viewModel.SecurityQueList2 = getSecurityQuestionList(2);
                    return View("SetSecurityQuestion", viewModel);
                }
                else
                {
                    var QueAns2 = new UserSecurityQue
                    {
                        UserId = viewModel.UserId,
                        Question = viewModel.Question2,
                        Answer = viewModel.Answer2,
                        UpdatedBy = UUID,
                        CreatedBy = UUID,
                    };
                    var response1 = await _securityQuestionService.CreateUserSecurityQnsAns(QueAns2);
                    if (response1 == null || !response1.Success)
                    {
                        AlertViewModel alert = new AlertViewModel { Message = (response1 == null ? "Internal error please contact to admin" : response1.Message) };
                        TempData["Alert"] = JsonConvert.SerializeObject(alert);
                        viewModel.SecurityQueList1 = getSecurityQuestionList(1);
                        viewModel.SecurityQueList2 = getSecurityQuestionList(2);
                        return View("SetSecurityQuestion", viewModel);
                    }
                    else
                    {
                        AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "Your Password and Security question set successfully" };
                        TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    }
                }
                return RedirectToAction("AccountActivateSuccess");
            }
            catch (Exception e)
            {
                _logger.LogInformation("SetSecurityQuestion post Exception : {0}", e.Message);
                ViewBag.error = "Internal Error";
                ViewBag.error_description = "Something went wrong, Please try again later";
                return View("Errorp");
            }
        }

        public async Task<IActionResult> ChangeSecurityQuestion()
        {
            var id = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData).Value);
            // var id = 1;
            var UserSecQues = await _securityQuestionService.GetUserSecurityQuestions(id);
            if (UserSecQues == null)
            {
                return NotFound();
            }
            else
            {
                var model = new SecurityQuestionViewModel()
                {
                    Que1Id = UserSecQues.Result[0].Id,
                    Question1 = UserSecQues.Result[0].Question,
                    Que2Id = UserSecQues.Result[1].Id,
                    Question2 = UserSecQues.Result[1].Question,
                    UserId = id,
                    SecurityQueList1 = getSecurityQuestionList(1),
                    SecurityQueList2 = getSecurityQuestionList(2)
                };
                _logger.LogInformation("<-- ChangeSecurityQuestion get");
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangeSecurityQuestion(SecurityQuestionViewModel viewModel)
        {


            var QueAns1 = new UserSecurityQue
            {
                Id = viewModel.Que1Id,
                UserId = viewModel.UserId,
                Question = viewModel.Question1,
                Answer = viewModel.Answer1,
                CreatedBy = UUID,
                UpdatedBy = UUID
            };
            var response = await _securityQuestionService.UpdateUserSecurityQnsAns(QueAns1);
            if (response == null || !response.Success)
            {
                viewModel.SecurityQueList1 = getSecurityQuestionList(1);
                viewModel.SecurityQueList2 = getSecurityQuestionList(2);

                AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return View("ChangeSecurityQuestion", viewModel);
            }
            else
            {
                var QueAns2 = new UserSecurityQue
                {
                    Id = viewModel.Que2Id,
                    UserId = viewModel.UserId,
                    Question = viewModel.Question2,
                    Answer = viewModel.Answer2,
                    CreatedBy = UUID,
                    UpdatedBy = UUID
                };
                var response1 = await _securityQuestionService.UpdateUserSecurityQnsAns(QueAns2);
                if (response1 == null || !response1.Success)
                {
                    viewModel.SecurityQueList1 = getSecurityQuestionList(1);
                    viewModel.SecurityQueList2 = getSecurityQuestionList(2);

                    AlertViewModel alert = new AlertViewModel { Message = (response1 == null ? "Internal error please contact to admin" : response1.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return View("ChangeSecurityQuestion", viewModel);
                }
                else
                {
                    AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "Security question updated successfully" };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return RedirectToAction("Profile");

                }

            }
        }
        public List<SelectListItem> getSecurityQuestionList(int listNo)
        {
            var security = new List<SelectListItem>();
            var names = Enum.GetNames(typeof(Core.Enums.SecurityQuesEnum));
            foreach (var name in names)
            {
                var field = typeof(Core.Enums.SecurityQuesEnum).GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                foreach (DescriptionAttribute fd in fds)
                {
                    if (listNo == 1)
                    {
                        var data = new SelectListItem();
                        data.Text = fd.Description;
                        data.Value = fd.Description;
                        if (name == "favouritecar")
                        {
                            data.Selected = true;
                        }
                        security.Add(data);
                    }
                    else
                    {
                        var data = new SelectListItem();
                        data.Text = fd.Description;
                        data.Value = fd.Description;
                        if (name == "favoritecity")
                        {
                            data.Selected = true;
                        }
                        security.Add(data);
                    }
                }
            }
            return security;
        }
        [HttpGet]
        public async Task<IActionResult> ValidateSecurityQuestion(string id)
        {
            try
            {
                _logger.LogInformation("--> ValidateSecurityQuestion get");
                if (string.IsNullOrEmpty(id))
                {
                    _logger.LogInformation("ValidateSecurityQuestion get : id value getting null");
                    ViewBag.error = "Invalid Request";
                    ViewBag.error_description = "Input value getting null";
                    return View("Errorp");
                }
                User Response = await _userService.GetUserByEmail(id); ;


                if (Response == null)
                {
                    _logger.LogInformation("ValidateSecurityQuestion get : getting user details response null");
                    ViewBag.error = "Internal Error";
                    ViewBag.error_description = "Something went wrong!";
                    return View("Errorp");
                }

                var UserStatus = Response.Status;
                if (UserStatus == "NEW")
                {
                    _logger.LogInformation("ValidateSecurityQuestion get : User status is new fail to get security question details");
                    ViewBag.error = "Invalid Request";
                    ViewBag.error_description = "User security question not configured.";
                    return View("Errorp");
                }

                var UserSecQues = await _securityQuestionService.GetUserSecurityQuestions(Response.Id);
                if (UserSecQues == null || !UserSecQues.Success)
                {
                    _logger.LogInformation("ValidateSecurityQuestion get :" + (UserSecQues != null ? UserSecQues.Message : " getting user sequrity question details response null"));
                    ViewBag.error = (UserSecQues != null ? UserSecQues.Message : "");
                    ViewBag.error_description = (UserSecQues != null ? UserSecQues.Message : "Something went wrong!");
                    return View("Errorp");
                }

                var model = new SecurityQuestionViewModel()
                {
                    Que1Id = UserSecQues.Result[0].Id,
                    Que2Id = UserSecQues.Result[1].Id,
                    UserId = Response.Id,
                    Username = Response.Name,
                    Question1 = UserSecQues.Result[0].Question,
                    Question2 = UserSecQues.Result[1].Question,
                };
                _logger.LogInformation("<-- ValidateSecurityQuestion get");
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogInformation("ValidateSecurityQuestion get Exception : {0}", e.Message);
                ViewBag.error = "Internal Error";
                ViewBag.error_description = "Something went wrong, Please try again later";
                return View("Errorp");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ValidateSecurityQuestion(SecurityQuestionViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("--> ValidateSecurityQuestion post");


                ValidateUserSecQueRequest data = new ValidateUserSecQueRequest();
                List<SecQuestionsAnswers> secQueList = new List<SecQuestionsAnswers>()
            {
                new SecQuestionsAnswers
                {
                    secQue = viewModel.Question1,
                    answer =viewModel.Answer1
                },
                new SecQuestionsAnswers
                {
                    secQue = viewModel.Question2,
                    answer =viewModel.Answer2
                },
            };


                data.secQueAns = secQueList;
                data.uuid = viewModel.UserId.ToString();

                var Response = await _securityQuestionService.ValidateUserSecurityQuestions(data);
                if (Response == null || !Response.Success)
                {
                    _logger.LogInformation("ValidateSecurityQuestion post : Security Question Validate Failed");
                    AlertViewModel alert = new AlertViewModel { Message = (Response == null ? "Internal error please contact to admin" : Response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return View("ValidateSecurityQuestion", viewModel);
                }
                else
                {
                    AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "Security Question Validate Successfully" };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);

                    return RedirectToAction("SetPassword", new { id = viewModel.UserId });
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation("ValidateSecurityQuestion post Exception : {0}", e.Message);
                ViewBag.error = "Internal Error";
                ViewBag.error_description = "Something went wrong, Please try again later";
                return View("Errorp");
            }
        }
        [HttpGet]
        public IActionResult ChangePassword(int id, bool isFirstLogin = false)
        {
            var model = new ChangePasswordViewModel()
            {
                Id = id,
                isFirstLogin = isFirstLogin
            };

            if (isFirstLogin)
            {
                return View("FirstChangePassword", model);
            }
            else
            {
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            try
            {


                var response = await _userPasswordService.ChangePassword(viewModel.Id,
                    viewModel.OldPassword, viewModel.NewPassword);
                if (response == null || !response.Success)
                {

                    AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);

                    if (viewModel.isFirstLogin)
                    {
                        return View("FirstChangePassword", viewModel);
                    }
                    else
                    {
                        return View("ChangePassword", viewModel);
                    }
                }
                else
                {
                    if (viewModel.isFirstLogin)
                    {
                        AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "Change User Password success" };
                        TempData["Alert"] = JsonConvert.SerializeObject(alert);
                        return RedirectToAction("AccountActivateSuccess");
                    }
                    else
                    {
                        AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "Change User Password success" };
                        TempData["Alert"] = JsonConvert.SerializeObject(alert);
                        return RedirectToAction("Profile");
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogInformation("ChangePassword post Exception : {0}", e.Message);
                ViewBag.error = "Internal Error";
                ViewBag.error_description = "Something went wrong, Please try again later";
                return View("Errorp");
            }
        }
        [HttpGet]
        public IActionResult SetPassword(int id)
        {
            try
            {
                _logger.LogInformation("--> SetPassword get");
                var model = new SetPasswordViewModel()
                {
                    Id = id,
                };
                _logger.LogInformation("<-- SetPassword get");
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogInformation("SetPassword get Exception : {0}", e.Message);
                ViewBag.error = "Internal Error";
                ViewBag.error_description = "Something went wrong, Please try again later";
                return View("Errorp");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel viewModel)
        {
            try
            {
                _logger.LogInformation("--> SetPassword post");


                var passwordData = new ResetPasswordRequest()
                {
                    userId = viewModel.Id,
                    newPassword = viewModel.NewPassword,
                };

                var response = await _userPasswordService.ResetPassword(viewModel.Id, viewModel.NewPassword);
                if (response == null || !response.Success)
                {
                    _logger.LogInformation("--> SetPassword post : reset user password fail");
                    AlertViewModel alert = new AlertViewModel { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    return View("SetPassword", viewModel);
                }
                else
                {
                    AlertViewModel alert = new AlertViewModel { IsSuccess = true, Message = "Reset User Password Successfully" };
                    TempData["Alert"] = JsonConvert.SerializeObject(alert);
                    _logger.LogInformation("<-- SetPassword post");
                    return RedirectToAction("AccountActivateSuccess");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation("SetPassword post Exception : {0}", e.Message);
                ViewBag.error = "Internal Error";
                ViewBag.error_description = "Something went wrong, Please try again later";
                return View("Errorp");
            }
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var UserinDb = await _userService.GetUserByEmail(email);
            if (UserinDb == null)
            {
                return View("ForgotPassword");
            }
            return RedirectToAction("ValidateSecurityQuestion", new { id = email });
        }
        [HttpGet]
        public IActionResult AccountActivateSuccess()
        {
            if (TempData.ContainsKey("isForgotPassword"))
            {
                ViewBag.isForgotPassword = TempData["isForgotPassword"].ToString();
            }
            if (TempData.ContainsKey("Alert"))
            {
                var alert = TempData["Alert"].ToString();

            }
            return View("AccountActivateSuccess");
        }
    }
}
