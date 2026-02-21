namespace EnterpriseGatewayPortal.Core.Domain.Lookups
{
    public class ActivityLookupItem : LookupItem
    {
        public bool McEnabled { get; set; }
        public bool McSupported { get; set; }
        public int ParentId { get; set; }
    }
}
