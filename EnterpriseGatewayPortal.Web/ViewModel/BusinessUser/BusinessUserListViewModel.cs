using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.BusinessUser
{
    public class BusinessUserListViewModel
    {
        //public IEnumerable<BusinessUserDTO> BusinesUser { get; set; }
        public IEnumerable<OrgSubscriberEmail> BusinesUser { get; set; }

    }
}
