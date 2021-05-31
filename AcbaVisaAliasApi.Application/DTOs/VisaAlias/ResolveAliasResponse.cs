using System.Collections.Generic;

namespace AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias
{
    public record ResolveAliasResponse(string Country, string RecipientPrimaryAccountNumber, string IssuerName, string CardType, string RecipientName, AccountLookUpInfo AccountLookUpInfo);

    public class VisaNetworkInfo
    {
        public string CardTypeCode { get; set; }
        public int BillingCurrencyCode { get; set; }
        public int BillingCurrencyMinorDigits { get; set; }
        public string IssuerName { get; set; }
        public int CardIssuerCountryCode { get; set; }
        public string FastFundsIndicator { get; set; }
        public string PushFundsBlockIndicator { get; set; }
        public string OnlineGambingBlockIndicator { get; set; }
    }

    public class AccountLookUpInfo
    {
        public List<VisaNetworkInfo> VisaNetworkInfo { get; set; }
    }
}
