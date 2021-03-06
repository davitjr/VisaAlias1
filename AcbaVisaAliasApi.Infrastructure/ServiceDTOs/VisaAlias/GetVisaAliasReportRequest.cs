namespace AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias
{
    public class GetVisaAliasReportRequest
    {
        /// <summary>
        /// This attribute is uniquely generated by Visa Alias directory to identify the report generation request. 
        /// This is used to retrieve the alias report together with the page ID.
        /// The format is {BID}-{TYPE}-{NUMERIC STRING}, where BID is the Business Identifier of the client used by Visa, 
        /// TYPE can be 'CONSUMER', 'MERCHANT' or 'AGENT' alias report.
        /// </summary>
        public string Reportid { get; set; }

        /// <summary>
        /// Numeric only. This attribute is used to specify the page number of the report.
        /// </summary>
        public string Pageid { get; set; }
    }
}
