namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class RateCardsController : BaseController
    //{

    //    private readonly IConfiguration _configuration;
    //    private readonly IRateCardsService _rateCardsService;
    //    private readonly ILogger<RateCardsController> _logger;

    //    public RateCardsController(IAdminLogService adminLogService, IRateCardsService rateCardsService, ILogger<RateCardsController> logger) : base(adminLogService)
    //    {
    //            _logger = logger;
    //            _rateCardsService = rateCardsService;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> Index()
    //    {
    //        string logMessage;
    //        var response = await _rateCardsService.GetAllRateCardsAsync();
    //        if (!response.Success)
    //        {
    //            logMessage = $"Failed to receive Rate Cards List";
    //            SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //                "Get Rate Card details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }
    //        var rateCards = (IEnumerable<RateCardsDTO>)response.Resource;
    //        var rates = rateCards.Where(o => o.StakeHolder == "ORGANIZATION").ToList();

    //        RateCardsListViewModel model = new RateCardsListViewModel()
    //        {
    //            RateCards = rates
    //        };

    //        logMessage = $"Successfully received Rate Cards List";
    //        SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //            "Get Rate Card details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(model);
    //    }
    //}
}
