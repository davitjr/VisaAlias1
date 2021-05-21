using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.Helpers;
using AcbaVisaAliasApi.Application.Http;
using AcbaVisaAliasApi.Application.Settings;
using AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias;
using AutoMapper;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.Services.AcbaVisaAlias
{
    public class VisaAliasService : IVisaAliasService
    {
        public VisaAliasService(IHttpRequestSender httpRequestSender, IMapper mapper,
            IOptions<VisaAliasApiOptions> VisaAliasOptions, ICryptographyHelper cryptographyHelper)
        {
            _httpRequestSender = httpRequestSender;
            _mapper = mapper;
            _VisaAliasOptions = VisaAliasOptions.Value;
            _cryptographyHelper = cryptographyHelper;
        }

        private readonly VisaAliasApiOptions _VisaAliasOptions;
        private readonly IHttpRequestSender _httpRequestSender;
        private readonly ICryptographyHelper _cryptographyHelper;
        private readonly IMapper _mapper;

        public async Task<GetAliasResponse> GetVisaAliasAsync(GetAliasRequest request)
        {
            GetVisaAliasRequest content = _mapper.Map<GetAliasRequest, GetVisaAliasRequest>(request);            
           Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.GetAliasApi, content, VisaAliasAction.GetVisaAlias, content.Guid);
            GetVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<GetVisaAliasResponse>(responseStream);
            return _mapper.Map<GetVisaAliasResponse, GetAliasResponse>(response);
        }

        public async Task<CreateAliasResponse> CreateVisaAliasAsync(CreateAliasRequest request)
        {
            CreateVisaAliasRequest content = _mapper.Map<CreateAliasRequest, CreateVisaAliasRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.CreateAliasApi, content , VisaAliasAction.CreateVisaAlias, content.Guid);
            CreateVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<CreateVisaAliasResponse>(responseStream);
            return _mapper.Map<CreateVisaAliasResponse, CreateAliasResponse>(response);
        }

        public async Task<UpdateAliasResponse> UpdateVisaAliasAsync(UpdateAliasRequest request)
        {
            UpdateVisaAliasRequest content = _mapper.Map<UpdateAliasRequest, UpdateVisaAliasRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.UpdateAliasApi, content, VisaAliasAction.UpdateVisaAlias, content.Guid);
            UpdateVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<UpdateVisaAliasResponse>(responseStream);
            return _mapper.Map<UpdateVisaAliasResponse, UpdateAliasResponse>(response);
        }

        public async Task<DeleteAliasResponse> DeleteVisaAliasAsync(DeleteAliasRequest request)
        {
            DeleteVisaAliasRequest content = _mapper.Map<DeleteAliasRequest, DeleteVisaAliasRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.DeleteAliasApi, content, VisaAliasAction.DeleteVisaAlias, content.Guid);
            DeleteVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<DeleteVisaAliasResponse>(responseStream);
            return _mapper.Map<DeleteVisaAliasResponse, DeleteAliasResponse>(response);
        }

        public async Task<GetAliasReportResponse> GetVisaAliasReportAsync(GetAliasReportRequest request)
        {
            Stream responseStream = await _httpRequestSender.SendGetRequest($"{_VisaAliasOptions.GetReportApi}/{request.Reportid}/{request.Pageid}", VisaAliasAction.GetVisaAliasReport, "");
            GetVisaAliasReportResponse response = await _cryptographyHelper.DecryptResponse<GetVisaAliasReportResponse>(responseStream);
            return _mapper.Map<GetVisaAliasReportResponse, GetAliasReportResponse>(response);
        }

        public async Task<GenerateAliasReportResponse> GenerateVisaAliasReportAsync(GenerateAliasReportRequest request)
        {
            GenerateVisaAliasReportRequest content = _mapper.Map<GenerateAliasReportRequest, GenerateVisaAliasReportRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.GenerateReportApi, content, VisaAliasAction.GenerateVisaAliasReport, "");
            GenerateVisaAliasReportResponse response = await _cryptographyHelper.DecryptResponse<GenerateVisaAliasReportResponse>(responseStream);
            return _mapper.Map<GenerateVisaAliasReportResponse, GenerateAliasReportResponse>(response);
        }

        public async Task<ResolveAliasResponse> ResolveVisaAliasAsync(ResolveAliasRequest request)
        {
            ResolveVisaAliasRequest content = _mapper.Map<ResolveAliasRequest, ResolveVisaAliasRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.ResolveApi, content, VisaAliasAction.ResolveVisaAlias, "");
            ResolveVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<ResolveVisaAliasResponse>(responseStream);
            ResolveAliasResponse resolveAliasResponse = _mapper.Map<ResolveVisaAliasResponse, ResolveAliasResponse>(response);
            return resolveAliasResponse;
        }

        public async Task<TestCredentialsResponse> TestCredentials()
        {
            Stream responseStream = await _httpRequestSender.SendGetRequest($"/vdp/helloworld", VisaAliasAction.NotDefined, "");
            var response = await JsonSerializer.DeserializeAsync<TestCredentialsResponse>(responseStream, DefaultJsonSettings.Settings);
            return response;
        }
    }
}
