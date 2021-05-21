namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{
    public class GenerateVisaAliasReportRequest
    {
        /// <summary>
        /// The start date of a report. Follows ISO 8601, date format YYYY-MM-DD. Example: 2019-01-01.
        /// If not set, it will be default to the previous date(T-1).
        /// </summary>
        public string ReportStartDate { get; set; }

        /// <summary>
        /// Maximum number of records to be contained in each page of the report.
        /// Default is set to a maximum of 20,000 records per page of a report.
        /// Minimum value is 1, Maximum value is 20,000.
        /// </summary>
        public string Limit { get; set; }

        /// <summary>
        /// Specify the type of alias records to be contained in the report.
        /// Valid values are CONSUMER, MERCHANT and AGENT.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// If not provided, default report contains alias records with all valid status, i.e.
        /// Consumer alias report will contain 'ACTIVE','INACTIVE' and 'DISABLED' records
        /// Merchant alias report will contain 'ACTIVE' and 'DISABLED' records
        /// Agent alias report will contain 'ACTIVE' and 'DISABLED' records.
        /// Valid values for status are 'ACTIVE', 'INACTIVE' and 'DISABLED'.
        /// </summary>
        public string Status { get; set; }
    }
}
