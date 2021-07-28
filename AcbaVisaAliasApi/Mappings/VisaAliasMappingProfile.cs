using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.DTOs.VisaAlias;
using AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias;
using AutoMapper;
using static AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias.GetVisaAliasResponse;

namespace AcbaVisaAliasApi.Mappings
{
    public class VisaAliasMappingProfile : Profile
    {
        public VisaAliasMappingProfile()
        {
            CreateMap<CreateAliasRequest, CreateVisaAliasRequest>();
            CreateMap<CreateVisaAliasResponse, CreateAliasResponse>();
            CreateMap<GetAliasRequest, GetVisaAliasRequest>();
            CreateMap<GetVisaAliasResponse, GetAliasResponse>();
            CreateMap<OneVisaAlias, OneAlias>();
            CreateMap<UpdateAliasRequest, UpdateVisaAliasRequest>();
            CreateMap<UpdateVisaAliasResponse, UpdateAliasResponse>();
            CreateMap<DeleteAliasRequest, DeleteVisaAliasRequest>();
            CreateMap<DeleteVisaAliasResponse, DeleteAliasResponse>();
            CreateMap<GetVisaAliasReportResponse, GetAliasReportResponse>();
            CreateMap<GenerateAliasReportRequest, GenerateVisaAliasReportRequest>();
            CreateMap<GenerateVisaAliasReportResponse, GenerateAliasReportResponse>();
            CreateMap<AliasReportLinks, Links>();
            CreateMap<AliasReportContent, Content>();
            CreateMap<ResolveAliasRequest, ResolveVisaAliasRequest>();
            CreateMap<VisaAliasNetworkInfo, VisaNetworkInfo>();
            CreateMap<VisaAliasAccountLookUpInfo, AccountLookUpInfo>();
            CreateMap<ResolveVisaAliasResponse, ResolveAliasResponse>();     
        }
    }
}

