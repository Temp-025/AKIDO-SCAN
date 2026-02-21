namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class PriceSlabDTO
    {
        public List<PricingSlabDefinition> PricingSlabDefinitionsList { get; set; }
        public double Rate { get; set; }
        public double Tax { get; set; }
    }


    public class PricingSlabDefinition
    {
        public int Id { get; set; }
        public ServiceDefinitions ServiceDefinitions { get; set; }
        public double Discount { get; set; }
        public double VolumeRangeFrom { get; set; }
        public double VolumeRangeTo { get; set; }
        public string StakeHolder { get; set; }
    }

    public class ServiceDefinitions
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDisplayName { get; set; }
        public string Status { get; set; }
        public bool Pricingslabapplicable { get; set; }
    }

    public class PriceSlabsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Result Result { get; set; }
    }

    
}
