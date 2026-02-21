namespace EnterpriseGatewayPortal.Web.ViewModel.BuyCredits
{
    public class CreditsSummeryViewModel
    {
        public double DS_TotalRate { get; set; }
        public double DS_Tax { get; set; }
        public double DS_TotalTax { get; set; }
        public double DS_Discount { get; set; }
        public double DS_TotalDiscount { get; set; }
        public double DS_TotalAmount { get; set; }
        public string DS_DisplayName { get; set; }
        public string DS_ServiceId { get; set; }
        public string DS_PrevialageServiceId { get; set; }

        // Eseal Signature properties
        public double ES_TotalsRate { get; set; }
        public double ES_Taxp { get; set; }
        public double ES_TotalTax { get; set; }
        public double ES_Discount { get; set; }
        public double ES_TotalDiscount { get; set; }
        public double ES_TotalAmount { get; set; }
        public string ES_DisplayName { get; set; }
        public string ES_ServiceId { get; set; }
        public string ES_PrevialageServiceId { get; set; }

        // User Subscription properties
        public double US_TotalsRate { get; set; }
        public double US_Taxper { get; set; }
        public double US_TotalTax { get; set; }
        public double US_Discountper { get; set; }
        public double US_TotalDiscount { get; set; }
        public double US_TotalAmount { get; set; }
        public string US_DisplayName { get; set; }
        public string US_ServiceId { get; set; }
        public string US_PrevialageServiceId { get; set; }

        //Credits
        public double DS_Credits {  get; set; }
        public double ES_Credits {  get; set; }
        public double US_Credits {  get; set; }

        //BasePrice
        public double DS_BasePrice { get; set; }
        public double ES_BasePrice {  get; set; }
        public double US_BasePrice {  get; set; }

        // Grand Total
        public double GrandTotal { get; set; }
        public string Org_Id { get; set; }

    }
}
