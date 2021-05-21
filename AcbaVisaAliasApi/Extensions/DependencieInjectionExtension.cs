using AcbaVisaAliasApi.Application.Helpers;
using AcbaVisaAliasApi.Application.Http;
using AcbaVisaAliasApi.Application.Settings;
using AcbaVisaAliasApi.Infrastructure.Services.AcbaVisaAlias;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
           .AddSingleton<IProblemDetailsHelper, ProblemDetailsHelper>();
        }
    }
}
