namespace EnterpriseGatewayPortal.Web.ViewModel.Beneficiaries
{
    public class BeneficiariesUserViewModel
    {
        
        public string SponsorType { get; set; }
        public string BeneficiaryType { get; set; }
        public string NationalIdNumber { get; set; }
        public string PassportNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmployeeEmail { get; set; }
        public string UgpassEmail { get; set; }
        public string SignaturePhoto { get; set; }
        public string Designation { get; set; }
        public List<SelectedValueViewModel> BeneficiaryValidities { get; set; }
    }

    public class SelectedValueViewModel
    {
        public string privilegeId { get; set; }
        public bool validityCheckbox { get; set; }
        public string validityFrom { get; set; }
        public string validityUpto { get; set; }

    }
}
