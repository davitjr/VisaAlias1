using System.Collections.Generic;

namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{

    public class ResolveVisaAliasResponse
    {
        /// <summary>
        /// Consumer’s country code as defined by ISO 3166, ISO 3166 alpha-2 is recommended to be used if alias may be used for QR.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Depending on the businessApplicationId of the request, this attribute can contain the consumer card number, 
        /// mVisa merchant ID (16-digit) or mVisa agent ID (16-digit)
        /// </summary>
        public string RecipientPrimaryAccountNumber { get; set; }

        /// <summary>
        /// Conditional. This is the issuer name of recipient’s card. Only applicable if businessApplicationId = “PP” or “CI”
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Card type description. Reference to Field 62.23—Product ID of available card products. e.g. Visa Platinum
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Depending on the businessApplicationId of the request, this attribute can contain the consumer name, 
        /// merchant name or agent name. Regarding consumer name, this is composed of consumer’s first, middle and last name
        /// </summary>
        public string RecipientName { get; set; }

        public VisaAliasAccountLookUpInfo AccountLookUpInfo { get; set; }
    }

    public class VisaAliasNetworkInfo
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

    public class VisaAliasAccountLookUpInfo
    {
        public List<VisaAliasNetworkInfo> VisaNetworkInfo { get; set; }
    }
}
