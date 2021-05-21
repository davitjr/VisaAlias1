using AcbaVisaAliasApi.Mappings;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AcbaVisaAliasApi.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapperConfigurations(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(VisaAliasMappingProfile));
        }
    }
}
