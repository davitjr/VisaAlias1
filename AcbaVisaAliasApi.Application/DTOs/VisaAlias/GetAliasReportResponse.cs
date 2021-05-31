using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias
{
    public record GetAliasReportResponse
    {
        [JsonPropertyName("_links")]
        public Links Links { get; init; }

        [JsonPropertyName("content")]
        public List<Content> Content { get; init; }
    }

    public record Links(string Next, string Last, string Self, string First);

    public record Content(string StatusChangeDateTime, string Guid, string Status);
}
