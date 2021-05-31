namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{
    public class ResolveVisaAliasRequest
    {
        /// <summary>
        ///Enum Values: * PP , * MP , * CO , * CI ,
        /// Used to identify program’s business application type for VisaNet transaction processing.
        /// It can be PP for Personal payment, MP for Merchant Payment, CO for Cash Out, CI for Cash In
        /// </summary>
        public string BusinessApplicationId { get; set; }

        /// <summary>
        /// This attribute is used to retrieve recipient's Primary Account Number (PAN) of the card holder which 
        /// can then be used in subsequent services such as Visa Direct.
        /// This attribute can contain information of a mobile phone number, email address, IBAN, merchant ID, etc.
        /// </summary>
        public string Alias { get; set; }

        public string AccountLookUp { get; set; }

    }
}
