using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class GraphDTO
    {
        public IEnumerable<WeekGraphDTO> WeekCount { get; set; }

        public IEnumerable<MonthGraphDTO> MonthCount { get; set; }

        public IEnumerable<YearGraphDTO> YearCount { get; set; }
    }
}
