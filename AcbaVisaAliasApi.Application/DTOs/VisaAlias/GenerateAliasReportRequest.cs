namespace AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias
{
    public record GenerateAliasReportRequest(string ReportStartDate, string Limit, string Type, string Status, int SetNumber);
}
