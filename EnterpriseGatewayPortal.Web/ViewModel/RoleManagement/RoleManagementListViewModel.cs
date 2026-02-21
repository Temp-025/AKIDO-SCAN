using System.Collections.Generic;

using EnterpriseGatewayPortal.Core.Domain.Lookups;


namespace EnterpriseGatewayPortal.Web.ViewModel.RoleManagement
{
    public class RoleManagementListViewModel
    {
        public IEnumerable<RoleLookupItem> RoleLookupItems { get; set; }
    }
}
