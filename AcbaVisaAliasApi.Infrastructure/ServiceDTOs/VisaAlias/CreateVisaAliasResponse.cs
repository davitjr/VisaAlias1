namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{
    public class CreateVisaAliasResponse
    {
        /// <summary>
        /// GUID of the consumer of issuer is returned if alias is created/updated/deleted successfully.
        /// </summary>
        public string Guid { get; set; }
    }
}
