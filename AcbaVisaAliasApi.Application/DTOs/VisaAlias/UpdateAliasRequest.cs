namespace AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias
{
    public record UpdateAliasRequest (string Guid, string ConsentDateTime, string Alias, string AliasType, 
         string RecipientPrimaryAccountNumber, string CardType, string IssuerName, string NewGuid, int SetNumber);
}
