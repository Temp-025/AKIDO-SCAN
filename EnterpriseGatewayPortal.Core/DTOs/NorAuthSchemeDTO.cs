using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseGatewayPortal.Core.DTOs
{
    public partial class NorAuthScheme
    {
        public int Id { get; set; }
        public int? AuthSchId { get; set; }
        public int? PriAuthSchId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual AuthScheme AuthSch { get; set; }
        public virtual PrimaryAuthScheme PriAuthSch { get; set; }
    }

    public partial class AuthScheme
    {
        public AuthScheme()
        {
            NorAuthSchemes = new HashSet<NorAuthScheme>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? PriAuthSchCnt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsPrimaryAuthscheme { get; set; }
        public string DisplayName { get; set; }
        public string Hash { get; set; }

        public virtual ICollection<NorAuthScheme> NorAuthSchemes { get; set; }
    }

    public partial class PrimaryAuthScheme
    {
        public PrimaryAuthScheme()
        {
            NorAuthSchemes = new HashSet<NorAuthScheme>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }
        public int ClientVerify { get; set; }
        public int StrngMatch { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int RandPresent { get; set; }
        public string Hash { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<NorAuthScheme> NorAuthSchemes { get; set; }
    }
}
