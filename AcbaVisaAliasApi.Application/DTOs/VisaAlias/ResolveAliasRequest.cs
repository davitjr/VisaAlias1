namespace AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias
{
    public record ResolveAliasRequest(string BusinessApplicationId, string Alias, string AccountLookUp, int SetNumber);
}
