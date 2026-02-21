using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services.Communication
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }

    public class BooleanResponse : BaseResponse<bool>
    {
        public BooleanResponse(bool category) : base(category) { }
        public BooleanResponse(string message) : base(message) { }
    }
}

