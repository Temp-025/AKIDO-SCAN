using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class OrgPriceSlabDTO
    {
        public OrgPriceSlabDTO()
        {
            ServiceDefinitions = new ServiceDefinitionDTO();
        }

        public ServiceDefinitionDTO ServiceDefinitions { get; set; }

        public int Id { get; set; }

        public double VolumeRangeFrom { get; set; }

        public double VolumeRangeTo { get; set; }

        public double Discount { get; set; }

        [JsonProperty("organizationId")]
        public string OrganizationUid { get; set; }

        public string OrganizationName { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string ApprovedBy { get; set; }
    }
}
