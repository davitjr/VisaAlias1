using System.Text.Json;

namespace AcbaVisaAliasApi.Application.Settings
{
    public static class DefaultJsonSettings
    {
        public static readonly JsonSerializerOptions Settings = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
