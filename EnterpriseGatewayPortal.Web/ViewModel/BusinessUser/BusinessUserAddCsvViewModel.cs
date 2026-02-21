using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.BusinessUser
{
    public class BusinessUserAddCsvViewModel
    {
        public IList<BusinessUserList> UserList { get; set; }

    }
}
