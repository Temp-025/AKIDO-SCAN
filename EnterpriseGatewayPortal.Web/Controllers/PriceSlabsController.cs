namespace EnterpriseGatewayPortal.Web.Controllers
{
    //[ServiceFilter(typeof(SessionValidationAttribute))]
    //[Authorize]
    //public class PriceSlabsController : BaseController
    //{
    //    private readonly IConfiguration _configuration;
    //    private readonly IPriceSlabService _priceSlabService;
    //    private readonly IOrganizationService _organizationService;
    //    private readonly ILogger<PriceSlabsController> _logger;
    //    public PriceSlabsController(IAdminLogService adminLogService, IPriceSlabService priceSlabService, IOrganizationService organizationService, ILogger<PriceSlabsController> logger) : base(adminLogService)
    //    {
    //        _logger = logger;
    //        _priceSlabService = priceSlabService;
    //        _organizationService = organizationService;
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> List1()
    //    {
    //        string logMessage;
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var priceSlabDefinitions = await _priceSlabService.GetAllPriceSlabDefinitionsAsync();
    //        if (priceSlabDefinitions == null)
    //        {

    //            logMessage = "Failed to get general price slabs list";
    //            SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //                "Get all Generic Price Slabs list", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            return NotFound();
    //        }

    //        PriceSlabDefinitionListViewModel viewModel = new PriceSlabDefinitionListViewModel();


    //        var list = priceSlabDefinitions.DistinctBy(x => new { x.Stakeholder, x.ServiceDefinitions.Id })
    //            .Where(y => y.Stakeholder == "ORGANIZATION")
    //         .Select(y => new { y.ServiceDefinitions.Id, y.ServiceDefinitions.ServiceDisplayName, y.Stakeholder, y.ServiceDefinitions.Status }).ToList();
    //        ;
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            viewModel.PriceSlabs.Add(new GenericPriceSlabViewModel { ServiceId = list[i].Id, ServiceName = list[i].ServiceDisplayName, Stakeholder = list[i].Stakeholder, Status = list[i].Status });
    //        }


    //        logMessage = "Successfully received general price slabs list";
    //        SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //            "Get all Generic Price Slabs list", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(viewModel);
    //    }

    //    [HttpGet]
    //    public async Task<IActionResult> List2()
    //    {
    //        string logMessage;
    //        var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);

    //        var orgPriceSlabDefinitions = await _priceSlabService.GetAllOrgPriceSlabDefinitionsAsync();

    //        var orgSpecificList = orgPriceSlabDefinitions.Where(o => o.OrganizationUid == organizationUid).ToList();

    //        if (orgPriceSlabDefinitions == null)
    //        {

    //            logMessage = "Failed to get organization Specific price slabs list";
    //            SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //                "Get all Organizations Price Slabs list", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);

    //            return NotFound();
    //        }

    //        PriceSlabDefinitionListViewModel viewModel = new PriceSlabDefinitionListViewModel();
    //        var list2 = orgSpecificList.DistinctBy(
    //          x => new { x.OrganizationUid, x.ServiceDefinitions.Id })
    //          .Select(y => new { y.ServiceDefinitions.Id, y.ServiceDefinitions.ServiceDisplayName, y.OrganizationUid, y.ServiceDefinitions.Status })
    //          .ToList();

    //        var organizations = await GetOrganizationList();

    //        for (int i = 0; i < list2.Count; i++)
    //        {
    //            viewModel.OrgPriceSlabs.Add(
    //                new OrgPriceSlabViewModel
    //                {
    //                    ServiceId = list2[i].Id,
    //                    ServiceName = list2[i].ServiceDisplayName,
    //                    OrganizationUid = list2[i].OrganizationUid,
    //                    OrganizationName = organizations.Where(x => x.Value == list2[i].OrganizationUid).Select(x => x.Text).SingleOrDefault(),
    //                    Status = list2[i].Status
    //                });
    //        }

    //        logMessage = "Successfully received organization Specific price slabs list";
    //        SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //            "Get all Organizations Price Slabs list", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(viewModel);

    //    }
    //        [HttpGet]
    //    public async Task<IActionResult> ViewGenericDetails(int serviceId, string stakeholder, string viewData)
    //    {
    //        string logMessage;
    //        var priceSlabs = await _priceSlabService.GetPriceSlabDefinitionAsync(serviceId, stakeholder);
    //        if (priceSlabs == null)
    //        {
    //            logMessage = "Failed to get General Price slabs Details";
    //            SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //                "Get all General Price Slabs Details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }

    //        ViewBag.viewData = viewData;
    //        GenericPriceSlabDetailsViewModel viewModel = new GenericPriceSlabDetailsViewModel();
    //        if (priceSlabs.Count() > 0)
    //        {
    //            PriceSlabDefinitionDTO priceSlab = priceSlabs[0];
    //            viewModel.ServiceId = priceSlab.ServiceDefinitions.Id;
    //            viewModel.ServiceDisplayName = priceSlab.ServiceDefinitions.ServiceDisplayName;
    //            viewModel.Stakeholder = priceSlab.Stakeholder;
    //            viewModel.CreatedBy = priceSlab.CreatedBy;

    //            for (int i = 0; i < priceSlabs.Count(); i++)
    //            {
    //                viewModel.DiscountVolumeRanges.Add(new DiscountVolumeRangeDTO
    //                {
    //                    Id = priceSlabs[i].Id,
    //                    VolumeRangeFrom = priceSlabs[i].VolumeRangeFrom,
    //                    VolumeRangeTo = priceSlabs[i].VolumeRangeTo,
    //                    Discount = priceSlabs[i].Discount
    //                });
    //            }
    //        }

    //        logMessage = "Successfully received general price slabs Details";
    //        SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //            "Get all Generic Price Slabs Details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(viewModel);
    //    }

    //    [HttpGet]

    //    public async Task<IActionResult> ViewOrgDetails(int serviceId, string viewData)
    //    {
    //        string logMessage;
    //       var organizationUid = (HttpContext.User.Claims.FirstOrDefault(c => c.Type == "OrganizationUid").Value);
    //        var priceSlabs = await _priceSlabService.GetOrgPriceSlabDefinitionAsync(serviceId, organizationUid);
    //        if (priceSlabs == null)
    //        {

    //            logMessage = "Failed to get organization Specific price slabs Details";
    //            SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //                "Get all Organizations Price Slabs Details", LogMessageType.FAILURE.ToString(), logMessage, UUID, Email);
    //            return NotFound();
    //        }

    //        ViewBag.viewOrgData = viewData;
    //        GenericPriceSlabDetailsViewModel viewModel = new GenericPriceSlabDetailsViewModel();
    //        if (priceSlabs.Count() > 0)
    //        {
    //            OrgPriceSlabDTO priceSlab = priceSlabs[0];
    //            viewModel.ServiceId = priceSlab.ServiceDefinitions.Id;
    //            viewModel.ServiceDisplayName = priceSlab.ServiceDefinitions.ServiceDisplayName;
    //            viewModel.OrganizationUID = organizationUid;
    //            viewModel.OrganizationName = OrganizationName;
    //            viewModel.CreatedBy = priceSlab.CreatedBy;

    //            for (int i = 0; i < priceSlabs.Count(); i++)
    //            {
    //                viewModel.DiscountVolumeRanges.Add(new DiscountVolumeRangeDTO
    //                {
    //                    Id = priceSlabs[i].Id,
    //                    VolumeRangeFrom = priceSlabs[i].VolumeRangeFrom,
    //                    VolumeRangeTo = priceSlabs[i].VolumeRangeTo,
    //                    Discount = priceSlabs[i].Discount
    //                });
    //            }
    //        }

    //        logMessage = "Successfully received organization Specific price slabs Details";
    //        SendAdminLog(ModuleNameConstants.PriceModel, ServiceNameConstants.PriceModel,
    //            "Get all Organizations Price Slabs Details", LogMessageType.SUCCESS.ToString(), logMessage, UUID, Email);

    //        return View(viewModel);
    //    }

    //    private async Task<IList<SelectListItem>> GetOrganizationList()
    //    {
    //        IList<SelectListItem> list = new List<SelectListItem>();

    //        var result = await _organizationService.GetOrganizationNamesAndIdAysnc();
    //        if (result == null)
    //        {
    //            return list;
    //        }
    //        else
    //        {
    //            foreach (var org in result)
    //            {
    //                var orgobj = org.Split(",");
    //                list.Add(new SelectListItem { Text = orgobj[0], Value = orgobj[1] });
    //            }

    //            return list;
    //        }
    //    }
    //}
}
