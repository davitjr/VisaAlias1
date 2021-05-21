namespace AcbaVisaAliasApi.Application.Settings
{
    public class VisaAliasApiOptions
    {
        public string UserID { get; init; }
        public string Password { get; init; }
        public string BaseUrl { get; init; }
        public string KeyId { get; init; }
        public string MleClientPrivateKeyPath { get; init; }
        public string MleServerCertificateThumbprint { get; init; }
        public string AuthorizationCertificateThumbprint { get; init; }
        public bool AllowInvalidCertificate { get; init; }
        public string GetAliasApi { get; init; }
        public string CreateAliasApi { get; init; }
        public string UpdateAliasApi { get; init; }
        public string DeleteAliasApi { get; init; }
        public string GetReportApi { get; init; }
        public string GenerateReportApi { get; init; }
        public string ResolveApi { get; init; }
        public bool EnableOkResponseLogging { get; init; }

    }
}
