namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{
    public class DeleteVisaAliasRequest
    {
        /// <summary>
        /// This attribute is uniquely used by issuer to identify their customer (i.e. consumer cardholder).
        /// Issuer may pass their existing unique identifier of a cardholder of their system to VAD as a guid.
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
       
    }
}
