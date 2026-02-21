namespace EnterpriseGatewayPortal.Web.ViewModel.Scopes
{
    public class ScopesListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool UserConsent { get; set; }
    }
}
