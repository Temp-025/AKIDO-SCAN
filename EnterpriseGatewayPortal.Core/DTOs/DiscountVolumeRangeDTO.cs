using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
        public class DiscountVolumeRangeDTO
        {
            public int Id { get; set; }

            public double VolumeRangeFrom { get; set; }

            public double VolumeRangeTo { get; set; }

            public double Discount { get; set; }
        }
    
}
