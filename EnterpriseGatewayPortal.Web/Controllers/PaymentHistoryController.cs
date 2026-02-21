namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class PaymentHistoryController : BaseController
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly ILogger<PaymentHistoryController> _logger;
    //    private readonly IPaymentHistoryService _paymentHistoryService;
    //    public PaymentHistoryController(IAdminLogService adminLogService, IPaymentHistoryService paymentHistoryService, ILogger<PaymentHistoryController> logger) : base(adminLogService)
    //    {
    //        _logger = logger;
    //        _paymentHistoryService = paymentHistoryService;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> OrgPaymentList()
    //    {
    //        string logMessage;
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var organizationPaymentHistory = await _paymentHistoryService.GetOrganizationPaymentHistoryAsync(organizationUid);
    //        if (organizationPaymentHistory == null)
    //        {

    //            logMessage = $"Failed to get payment history.";
    //            SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
    //                "Get Organization Payment History", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            Models.AlertViewModel alert = new Models.AlertViewModel { IsSuccess = false, Message = "Payment history not found" };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //            return RedirectToAction("Index", "Dashboard");
    //        }

    //        logMessage = $"Successfully received payment history.";
    //        SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
    //            "Get Organization Payment History", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(organizationPaymentHistory);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> GetPaymentInfo(string paymentInfo)
    //    {
    //        var viewModel = JsonConvert.DeserializeObject<IList<OrganizationPaymentInfoViewModel>>(paymentInfo);
    //        foreach (var item in viewModel)
    //        {
    //            item.ServiceDisplayName = (await _paymentHistoryService.GetServiceDefinitionsAsync()).Where(x => x.Id == Convert.ToInt32(item.ServiceId)).Select(x => x.ServiceDisplayName).SingleOrDefault();
    //        }

    //        return PartialView("_PaymentInfo", viewModel);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> View(string ackId)
    //    {
    //        string logMessage;
    //        var res = await _paymentHistoryService.GetPaymentInfoByTransactionRefId(ackId);
    //        if (!res.Success)
    //        {
    //            logMessage = $"Failed to get payment history view.";
    //            SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
    //                "Get Organization Payment History view", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            Models.AlertViewModel alert = new Models.AlertViewModel { IsSuccess = false, Message = "Payment history view not found" };
    //            TempData["Alert"] = JsonConvert.SerializeObject(alert);

    //            return RedirectToAction("OrgPaymentList", "PaymentHistory");
    //        }
    //        var paymentInfo = (IEnumerable<OrganizationPaymentHistoryDTO>)res.Resource;
    //        var info = paymentInfo.FirstOrDefault();

    //        PaymentHistoryInfoViewModel model = new PaymentHistoryInfoViewModel()
    //        {
    //            PaymentChannel = info.PaymentChannel,
    //            TotalAmount = info.TotalAmount,
    //            CreatedOn = info.CreatedOn.ToString("yyyy-MM-dd"),
    //            TransactionReferenceId = info.TransactionReferenceId,
    //            AggregatorAcknowledgementId = info.AggregatorAcknowledgementId,
    //            PaymentStatus = info.PaymentStatus

    //        };
    //        logMessage = $"Successfully viewed payment history.";
    //        SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
    //            "Get Organization Payment History view", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(model);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> CheckPaymentStatus(string id)
    //    {
    //        try
    //        {
    //            string logMessage;
    //            var res = await _paymentHistoryService.GetPaymentInfoByTransactionRefId(id);
    //            if (!res.Success)
    //            {
    //                logMessage = $"Failed to get payment status.";
    //                SendAdminLog(ModuleNameConstants.Payments, ServiceNameConstants.Payments,
    //                    "Get Organization Payment History status", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //                return Json(new { success = false, message = "Failed to get payment status" });
    //            }
    //            var paymentInfo = (IEnumerable<OrganizationPaymentHistoryDTO>)res.Resource;
    //            var info = paymentInfo.FirstOrDefault();

    //            return Json(new { success = true, message = res.Message, statusInfo = info.PaymentStatus });
    //        }
    //        catch (Exception)
    //        {
    //            return Json(new { success = false, message = "Error While Processing" });
    //        }
    //    }
    //}
}
