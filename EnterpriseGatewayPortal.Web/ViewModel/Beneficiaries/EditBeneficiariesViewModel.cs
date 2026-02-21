namespace EnterpriseGatewayPortal.Web.ViewModel.Beneficiaries
{
    public class EditBeneficiariesViewModel
    {
        public int Id { get; set; }
        public string SponsorType { get; set; }
        public string BeneficiaryType { get; set; }
        public string NationalIdNumber { get; set; }
        public string PassportNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmployeeEmail { get; set; }
        public string UgpassEmail { get; set; }
        public string SignaturePhoto { get; set; }
        public string Designation { get; set; }
        public List<SelectedValueEditViewModel> BeneficiaryEditValidities { get; set; }
    }

    public class SelectedValueEditViewModel
    {
        public string privilegeId { get; set; }
        public bool validityCheckbox { get; set; }
        public string validityFrom { get; set; }
        public string validityUpto { get; set; }

    }
}
