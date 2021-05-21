namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{
    public class GenerateVisaAliasReportResponse
    {
        /// <summary>
        /// A URL link that contains a report ID and a page ID. Use GetReport API to navigate and download the report.
        /// </summary>
        public string ReportLocation { get; set; }
    }
}
