using AcbaVisaAliasApi.Application.Helpers;
using AcbaVisaAliasApi.Infrastructure.Http;
using AcbaVisaAliasApi.Application.Settings;
using AcbaVisaAliasApi.Infrastructure.DBManager;
using AcbaVisaAliasApi.Infrastructure.Services.AcbaVisaAlias;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AcbaVisaAliasApi.Extensions
{
    public static class DependencieInjectionExtension
    {

        public static void AddDependencieInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<VisaAliasApiOptions>(configuration.GetSection(nameof(VisaAliasApiOptions)))
           .AddScoped<IHttpRequestSender, VisaHttpRequestSender>()
           .AddScoped<IHttpResponseHandler, HttpResponseStatusChecker>()
           .AddScoped<IVisaAliasService, VisaAliasService>()
           .AddScoped<ICryptographyHelper, CryptographyHelper>()
           .AddScoped<IVisaAliasDB, VisaAliasDB>()
           .AddSingleton<IProblemDetailsHelper, ProblemDetailsHelper>();

        }
    }
}
