using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.Services.AcbaVisaAlias
{
    public interface IVisaAliasService
    {
        Task<GetAliasResponse> GetVisaAliasAsync(GetAliasRequest request);
        Task<CreateAliasResponse> CreateVisaAliasAsync(CreateAliasRequest request);
        Task<UpdateAliasResponse> UpdateVisaAliasAsync(UpdateAliasRequest request);
        Task<DeleteAliasResponse> DeleteVisaAliasAsync(DeleteAliasRequest request);
        Task<GetAliasReportResponse> GetVisaAliasReportAsync(GetAliasReportRequest request);
        Task<GenerateAliasReportResponse> GenerateVisaAliasReportAsync(GenerateAliasReportRequest request);
        Task<ResolveAliasResponse> ResolveVisaAliasAsync(ResolveAliasRequest request);
        Task<TestCredentialsResponse> TestCredentials();
    }
}