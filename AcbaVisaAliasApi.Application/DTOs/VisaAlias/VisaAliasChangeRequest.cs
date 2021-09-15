namespace AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias
{
    public record VisaAliasChangeRequest(ulong CustomerNumber, string Alias, Action Action);

    public enum Action
    {
        Update = 1,
        Delete = 2
    }
}

