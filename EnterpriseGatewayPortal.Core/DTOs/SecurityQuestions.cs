using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public class SecurityQuestions
    {
        public int Id { get; set; }
        public string Question { get; set; }
    }
    public class GetAllUserSecurityQueResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public IList<SecurityQuestions> Result { get; set; }
    }
    public class ValidateUserSecQueResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TemporarySession { get; set; }
        //public IList<string> AuthenticationSchemes { get; set; }
    }
}
