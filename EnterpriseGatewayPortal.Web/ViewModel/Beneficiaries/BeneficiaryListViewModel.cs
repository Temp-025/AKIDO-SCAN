using EnterpriseGatewayPortal.Core.Domain.Models;
using EnterpriseGatewayPortal.Core.DTOs;

namespace EnterpriseGatewayPortal.Web.ViewModel.Beneficiaries
{
    public class BeneficiaryListViewModel
    {
       // public IEnumerable<SponsorBeneficiaryDTO> BeneficiaryList { get; set; }
        public IEnumerable<Beneficiary> BeneficiaryList { get; set; }
        public IEnumerable<RawCsvDataDTO> CsvData { get; set; }
    }
}
