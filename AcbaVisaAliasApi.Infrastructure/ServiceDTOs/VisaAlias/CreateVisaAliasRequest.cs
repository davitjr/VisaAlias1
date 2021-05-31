namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{
    public class CreateVisaAliasRequest
    {
        /// <summary>
        /// Consumer’s country code as defined by ISO 3166, ISO 3166 alpha-2 is recommended to be used if alias may be used for QR.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Consumer’s last name.
        /// </summary>
        public string RecipientLastName { get; set; }

        /// <summary>
        /// Consumer’s first name.
        /// </summary>
        public string RecipientFirstName { get; set; }

        /// <summary>
        /// Consumer's card number.
        /// </summary>
        public string RecipientPrimaryAccountNumber { get; set; }

        /// <summary>
        /// This is the Issuer name of recipient’s card 
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Card type description. Reference to Field 62.23—Product ID of available card products. e.g. Visa Platinum
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Date  and time when consumer has provided their consent to Issuer about the use of their personal data for 
        /// VAD service. Format: YYYY-MM-DD hh:mm:ss. Local date and time should be converted to Coordinated 
        /// Universal Time (UTC) before submitting this value in API request.
        /// </summary>
        public string ConsentDateTime { get; set; }

        /// <summary>
        /// “01” – Phone number
        /// “02” – Email address
        /// “03” – National ID
        /// “04” – IBAN (International Bank Account Number)
        /// </summary>
        public string AliasType { get; set; }

        /// <summary>
        /// This attribute is uniquely used by Issuer to identify their customer(i.e.consumer cardholder).
        /// Issuer may pass their existing unique identifier of a cardholder of their system to VAD as a guid.
        /// For multiple aliases linked to the same PAN, same guid should be used except changing the field alias in the request payload.Below is an example:
        /// guid abcd12345678z01, alias 85291112222
        /// guid abcd12345678z01, alias 85288881111.
        /// For selected countries, one alias can be linked to more than one account, e.g.one mobile can be linked to three different cards with a default card to receive money. It is recommended that issuer to use systematic naming to generate guid for easy maintenance. Below is an example of one mobile number linking to three different PANs:
        /// alias 85291112222, guid abcd12345678z01 for PAN1 (default)
        /// alias 85291112222, guid abcd12345678z02 for PAN2
        /// alias 85291112222, guid abcd12345678z03 for PAN3, etc.
        /// </summary>
        public string Guid { get; set; }


        /// <summary>
        /// This attribute contains the alias data, e.g. phone number, email address, etc.,
        /// If phone number is used for alias, this should be provided in accordance with ITU-T E.164 (2010) 
        /// number structure.Below are some examples of phone numbers with different country codes:
        /// Country	Country Code	Examples
        /// United States	1	1650xxxxxxx
        /// Russia	7	7495xxxxxxx
        /// United Kingdom	44	4478xxxxxxxx
        /// Singapore	65	659xxxxxxx
        /// Hong Kong	852	8529xxxxxxx
        /// Kenya	254	254701xxxxxx
        /// </summary>
        public string Alias { get; set; }

        public string ExpiryDate { get; set; }

    }
}
