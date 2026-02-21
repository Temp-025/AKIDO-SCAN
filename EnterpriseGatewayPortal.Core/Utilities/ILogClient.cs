using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EnterpriseGatewayPortal.Core.Domain.Services.Communication.CommonResponse;

namespace EnterpriseGatewayPortal.Core.Utilities
{
    public interface ILogClient
    {
        public int SendAdminLogMessage(string adminLogMessage);
        
    }
}
