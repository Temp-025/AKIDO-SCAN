using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseGatewayPortal.Web.Enums
{
    public enum ModuleName
    {
        //[Display(Name = "Dashboard")]
        //[EnumMember(Value = "DASHBOARD")]
        //DASHBOARD,

        [Display(Name = "Organization")]
        [EnumMember(Value = "ORGANIZATION")]
        ORGANIZATION,

        [Display(Name = "Applications")]
        [EnumMember(Value = "APPLICATIONS")]
        APPLICATIONS,

        [Display(Name = "Signature Templates")]
        [EnumMember(Value = "SIGNATURE_TEMPLATES")]
        SIGNATURE_TEMPLATES,

        [Display(Name = "Business Users")]
        [EnumMember(Value = "BUSINESS_USERS")]
        BUSINESS_USERS,

        [Display(Name = "ESeal")]
        [EnumMember(Value = "ESEAL")]
        ESEAL,

        [Display(Name = "Document Templates")]
        [EnumMember(Value = "DOCUMENT_TEMPLATES")]
        DOCUMENT_TEMPLATES,

        [Display(Name = "Users")]
        [EnumMember(Value = "USERS")]
        USERS,

        [Display(Name = "Admin Reports")]
        [EnumMember(Value = "ADMIN_REPORTS")]
        ADMIN_REPORTS

    }
}
