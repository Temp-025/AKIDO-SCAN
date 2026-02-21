using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Exceptions
{
    public class PKIException : BaseException
    {

        public PKIException() : base()
        {
        }

        public PKIException(string message) : base(message)
        {
        }

        public PKIException(string message, Exception ex) : base(message, ex)
        {
        }

        public PKIException(string message, int statusCode) : base(message, statusCode)
        {
        }
    }
}
