namespace AcbaVisaAliasApi.Infrastructure
{
    public enum VisaAliasAction : byte
    {
        NotDefined = 0,
        GetVisaAlias = 1,
        CreateVisaAlias = 2,
        UpdateVisaAlias = 3,
        DeleteVisaAlias = 4,
        GetVisaAliasReport = 5,
        GenerateVisaAliasReport = 6,
        ResolveVisaAlias = 7
    }
}
