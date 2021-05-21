using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{
    public class GetVisaAliasReportResponse
    {
        [JsonPropertyName("_links")]
        public AliasReportLinks Links { get; set; }

        [JsonPropertyName("content")]
        public List<AliasReportContent> Content { get; set; }
    }
    public class AliasReportLinks
    {
        /// <summary>
        /// This attribute is the url to retrieve the content of first page of the report.
        /// </summary>
        public string First { get; set; }

        /// <summary>
        /// This attribute is the url to retrieve the content of last page of the report.
        /// </summary>
        public string Last { get; set; }

        /// <summary>
        /// This attribute is the url to retrieve the content of previous page of the report if available.
        /// </summary>
        public string Previous { get; set; }

        /// <summary>
        /// This attribute is the url to retrieve the content of previous page of the report if available
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// This attribute is the url to retrieve the content of current page of the report.
        /// </summary>
        public string Self { get; set; }
    }

    /// <summary>
    /// Depending on the selected type of alias reports, this contains a list of consumer, merchant or agent alias records.
    /// </summary>
    public class AliasReportContent
    {
        /// <summary>
        /// The date and time when the status of an alias is changed. It is in Coordinated Universal Time (UTC).
        /// </summary>
        public string StatusChangeDateTime { get; set; }

        /// <summary>
        /// Conditional. Only applicable for consumer alias report. This attribute is uniquely used by issuer to identify their cardholders (i.e. consumer).
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// The status of an alias.
        /// For consumer alias, this value can be 'ACTIVE', 'DISABLED' or 'INACTIVE'.
        /// For merchant/agent alias, this value can be 'ACTIVE' or 'DISABLED'.
        /// </summary>
        public string Status { get; set; }
    }
}
