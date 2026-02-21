namespace EnterpriseGatewayPortal.Web.ViewModel.Priceslabs
{
    public class PriceSlabDefinitionListViewModel
    {
        public PriceSlabDefinitionListViewModel()
        {
            PriceSlabs = new List<GenericPriceSlabViewModel>();
            OrgPriceSlabs = new List<OrgPriceSlabViewModel>();
        }

        public IList<GenericPriceSlabViewModel> PriceSlabs { get; set; }
        public IList<OrgPriceSlabViewModel> OrgPriceSlabs { get; set; }

        public string slabName { get; set; }
    }

    public class GenericPriceSlabViewModel
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string Stakeholder { get; set; }

        public string Status { get; set; }
    }

    public class OrgPriceSlabViewModel
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string OrganizationUid { get; set; }

        public string OrganizationName { get; set; }

        public string Status { get; set; }
    }
}
