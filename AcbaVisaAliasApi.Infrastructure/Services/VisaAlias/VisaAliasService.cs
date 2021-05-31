using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.DTOs.VisaAlias;
using AcbaVisaAliasApi.Application.Helpers;
using AcbaVisaAliasApi.Infrastructure.Http;
using AcbaVisaAliasApi.Application.Settings;
using AcbaVisaAliasApi.Infrastructure.DBManager;
using AcbaVisaAliasApi.Infrastructure.Enums;
using AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.Services.AcbaVisaAlias
{
    public class VisaAliasService : IVisaAliasService
    {
        public VisaAliasService(IHttpRequestSender httpRequestSender, IMapper mapper,
            IOptions<VisaAliasApiOptions> VisaAliasOptions, ICryptographyHelper cryptographyHelper, IVisaAliasDB visaAliasDB)
        {
            _httpRequestSender = httpRequestSender;
            _mapper = mapper;
            _VisaAliasOptions = VisaAliasOptions.Value;
            _cryptographyHelper = cryptographyHelper;
            _visaAliasDB = visaAliasDB;
        }

        private readonly VisaAliasApiOptions _VisaAliasOptions;
        private readonly IHttpRequestSender _httpRequestSender;
        private readonly ICryptographyHelper _cryptographyHelper;
        private readonly IMapper _mapper;
        private readonly IVisaAliasDB _visaAliasDB;

        public async Task<GetAliasResponse> GetVisaAliasAsync(GetAliasRequest request)
        {
            GetVisaAliasRequest content = _mapper.Map<GetAliasRequest, GetVisaAliasRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.GetAliasApi, content);
            GetVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<GetVisaAliasResponse>(responseStream);
            return _mapper.Map<GetVisaAliasResponse, GetAliasResponse>(response);
        }

        public async Task<CreateAliasResponse> CreateVisaAliasAsync(CreateAliasRequest request)
        {
            CreateVisaAliasRequest content = _mapper.Map<CreateAliasRequest, CreateVisaAliasRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.CreateAliasApi, content);
            CreateVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<CreateVisaAliasResponse>(responseStream);
            await _visaAliasDB.InsertVisaAliasactionHisoty(request, (short)VisaAliasActionTypes.createalias);
            return _mapper.Map<CreateVisaAliasResponse, CreateAliasResponse>(response);
        }

        public async Task<UpdateAliasResponse> UpdateVisaAliasAsync(UpdateAliasRequest request)
        {
            UpdateVisaAliasRequest content = _mapper.Map<UpdateAliasRequest, UpdateVisaAliasRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.UpdateAliasApi, content);
            UpdateVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<UpdateVisaAliasResponse>(responseStream);
            await _visaAliasDB.InsertVisaAliasactionHisoty(request, (short)VisaAliasActionTypes.updatealias);
            return _mapper.Map<UpdateVisaAliasResponse, UpdateAliasResponse>(response);
        }

        public async Task<DeleteAliasResponse> DeleteVisaAliasAsync(DeleteAliasRequest request)
        {
            DeleteVisaAliasRequest content = _mapper.Map<DeleteAliasRequest, DeleteVisaAliasRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.DeleteAliasApi, content);
            DeleteVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<DeleteVisaAliasResponse>(responseStream);
            await _visaAliasDB.InsertVisaAliasactionHisoty(request, (short)VisaAliasActionTypes.deletealias);
            return _mapper.Map<DeleteVisaAliasResponse, DeleteAliasResponse>(response);
        }

        public async Task<GetAliasReportResponse> GetVisaAliasReportAsync(GetAliasReportRequest request)
        {
            Stream responseStream = await _httpRequestSender.SendGetRequest($"{_VisaAliasOptions.GetReportApi}/{request.Reportid}/{request.Pageid}");
            GetVisaAliasReportResponse response = await _cryptographyHelper.DecryptResponse<GetVisaAliasReportResponse>(responseStream);
            return _mapper.Map<GetVisaAliasReportResponse, GetAliasReportResponse>(response);
        }

        public async Task<GenerateAliasReportResponse> GenerateVisaAliasReportAsync(GenerateAliasReportRequest request)
        {
            GenerateVisaAliasReportRequest content = _mapper.Map<GenerateAliasReportRequest, GenerateVisaAliasReportRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.GenerateReportApi, content);
            GenerateVisaAliasReportResponse response = await _cryptographyHelper.DecryptResponse<GenerateVisaAliasReportResponse>(responseStream);
            return _mapper.Map<GenerateVisaAliasReportResponse, GenerateAliasReportResponse>(response);
        }

        public async Task<ResolveAliasResponse> ResolveVisaAliasAsync(ResolveAliasRequest request)
        {
            ResolveVisaAliasRequest content = _mapper.Map<ResolveAliasRequest, ResolveVisaAliasRequest>(request);
            Stream responseStream = await _httpRequestSender.SendPostRequest(_VisaAliasOptions.ResolveApi, content);
            ResolveVisaAliasResponse response = await _cryptographyHelper.DecryptResponse<ResolveVisaAliasResponse>(responseStream);
            ResolveAliasResponse resolveAliasResponse = _mapper.Map<ResolveVisaAliasResponse, ResolveAliasResponse>(response);
            return resolveAliasResponse;
        }

        public async Task<TestCredentialsResponse> TestCredentials()
        {
            Stream responseStream = await _httpRequestSender.SendGetRequest($"/vdp/helloworld");
            TestCredentialsResponse response = await JsonSerializer.DeserializeAsync<TestCredentialsResponse>(responseStream, DefaultJsonSettings.Settings);
            return response;
        }

        public async Task<List<VisaAliasActionHistoryResponse>> GetVisaAliasHistory(VisaAliasHistoryRequest request)
        {
           return await _visaAliasDB.GetVisaAliasHistory(request.CustomerNumber);
        }
    }
}
