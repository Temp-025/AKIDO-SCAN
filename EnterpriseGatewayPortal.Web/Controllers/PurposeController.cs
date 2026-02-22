using EnterpriseGatewayPortal.Core.Domain.Services;
using EnterpriseGatewayPortal.Core.DTOs;
using EnterpriseGatewayPortal.Web.ViewModel;
using EnterpriseGatewayPortal.Web.ViewModel.Purposes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnterpriseGatewayPortal.Web.Controllers
{
    public class PurposeController : BaseController
    {
        private readonly IPurposeService _purposeService;
        public PurposeController(IAdminLogService adminLogService, IPurposeService purposeService) : base(adminLogService)
        {
            _purposeService = purposeService;
        }
        [HttpGet]
        public IActionResult New()
        {
            var ViewModel = new PurposeNewViewModel();
            return View(ViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var ViewModel = new List<PurposeListViewModel>();

            var PurposeList = await _purposeService.GetPurposesListAsync();

            if (PurposeList == null)
            {
                return NotFound();
            }

            foreach (var purpose in PurposeList)
            {
                var PurposeListViewModel = new PurposeListViewModel
                {
                    Id = purpose.Id,
                    Name = purpose.Name,
                    DisplayName = purpose.DisplayName,
                    Description = purpose.Description,
                    UserConsent = purpose.UserConsentRequired
                };
                ViewModel.Add(PurposeListViewModel);
            }
            return View(ViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var purposeinDb = await _purposeService.GetPurposeByIdAsync(id);

            if (purposeinDb == null)
            {
                return NotFound();
            }
            var ViewModel = new PurposeEditViewModel
            {
                Id = purposeinDb.Id,
                Name = purposeinDb.Name,
                DisplayName = purposeinDb.DisplayName,
                Description = purposeinDb.Description,
                UserConsent = purposeinDb.UserConsentRequired,
                Status = purposeinDb.Status
            };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(PurposeNewViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("New", ViewModel);
            }
            var purpose = new PurposesDTO()
            {
                Name = ViewModel.Name,
                DisplayName = ViewModel.DisplayName,
                Description = ViewModel.Description,
                UserConsentRequired = ViewModel.UserConsent,
                CreatedBy = UUID,
                UpdatedBy = UUID
            };
            var response = await _purposeService.CreatePurposeAsync(purpose);
            if (response == null || !response.Success)
            {
                Alert alert = new Alert { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return View("New", ViewModel);
            }
            else
            {
                Alert alert = new Alert { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("List");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PurposeEditViewModel ViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", ViewModel);
            }
            var purposeInDb = await _purposeService.GetPurposeByIdAsync(ViewModel.Id);
            if (purposeInDb == null)
            {
                return View("Edit", ViewModel);
            }
            purposeInDb.Id = ViewModel.Id;
            purposeInDb.Name = ViewModel.Name;
            purposeInDb.DisplayName = ViewModel.DisplayName;
            purposeInDb.Description = ViewModel.Description;
            purposeInDb.UserConsentRequired = ViewModel.UserConsent;
            purposeInDb.UpdatedBy = UUID;
            var response = await _purposeService.UpdatePurposeDataAsync(purposeInDb);
            if (response == null || !response.Success)
            {
                Alert alert = new Alert { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return View("Edit", ViewModel);
            }
            else
            {
                Alert alert = new Alert { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return RedirectToAction("List");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _purposeService.DeletePurposeAsync(id);
            if (response != null && response.Success)
            {

                Alert alert = new Alert { IsSuccess = true, Message = response.Message };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return new JsonResult(true);
            }
            else
            {
                Alert alert = new Alert { Message = (response == null ? "Internal error please contact to admin" : response.Message) };
                TempData["Alert"] = JsonConvert.SerializeObject(alert);
                return new JsonResult(false);
            }
        }
    }
}
