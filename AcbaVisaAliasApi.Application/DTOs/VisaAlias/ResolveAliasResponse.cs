namespace AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias
{
    public record ResolveAliasResponse(string Country, string RecipientPrimaryAccountNumber, string IssuerName, string CardType, string RecipientName);
}
