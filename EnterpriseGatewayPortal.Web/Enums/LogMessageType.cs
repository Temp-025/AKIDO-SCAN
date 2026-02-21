using System.Runtime.Serialization;

namespace EnterpriseGatewayPortal.Web.Enums
{
    public enum LogMessageType
    {
        [EnumMember(Value = "ERROR")]
        Error,

        [EnumMember(Value = "INFO")]
        Info,

        [EnumMember(Value = "WARNING")]
        Warning,

        [EnumMember(Value = "SUCCESS")]
        SUCCESS,

        [EnumMember(Value = "FAILURE")]
        FAILURE
    }
}
