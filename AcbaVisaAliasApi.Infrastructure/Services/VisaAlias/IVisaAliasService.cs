using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.DTOs.VisaAlias;
using AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias;
using System.Collections.Generic;
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
        Task<List<VisaAliasActionHistoryResponse>> GetVisaAliasHistory(VisaAliasHistoryRequest request);
        Task<DeleteVisaAliasRequest> GetVisaAliasForDeleteWithCard(DeleteVisaAliasWithCardRequest deleteVisaAliasWithCardRequest);
        Task<VisaAliasActionHistoryResponse> GetVisaAliasHistoryWithCardAsync(VisaAliasHistoryWithCard request);
        Task<UpdateAliasResponse> UpdateVisaAliasByPhoneNumberAsync(VisaAliasChangeRequest request);
        Task<DeleteAliasResponse> DeleteVisaAliasByPhoneNumberAsync(VisaAliasChangeRequest changeRequest);
    }
}