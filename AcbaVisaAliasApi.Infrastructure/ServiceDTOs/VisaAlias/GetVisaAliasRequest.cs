namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{
    public class GetVisaAliasRequest
    {
        /// <summary>
        /// This attribute is uniquely used by Issuer to identify their customer (i.e. consumer cardholder).
        /// </summary>
        public string Guid { get; set; }
    }
}
