using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.Domain.Services.Communication
{
    public class SecQuestionsAnswers
    {
        public string secQue { get; set; }
        public string answer { get; set; }
    }

    public class ValidateUserSecQueRequest
    {
        public string uuid { get; set; }
        public IList<SecQuestionsAnswers> secQueAns { get; set; }
    }
    public class ResetPasswordRequest
    {
        public int userId { get; set; }
        public string newPassword { get; set; }
        
    }
}
