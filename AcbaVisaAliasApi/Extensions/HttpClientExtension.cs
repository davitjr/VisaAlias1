using AcbaVisaAliasApi.Application.Helpers;
using AcbaVisaAliasApi.Application.Http;
using AcbaVisaAliasApi.Application.Settings;
using AutoWrapper.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AcbaVisaAliasApi.Extensions
{
    public static class HttpClientExtension
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            VisaAliasApiOptions visaAliasApiOptions = configuration.GetSection(nameof(VisaAliasApiOptions)).Get<VisaAliasApiOptions>();
            services.AddHttpClient(nameof(VisaHttpRequestSender), client =>
            {
                client.BaseAddress = new Uri(visaAliasApiOptions.BaseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetBasicAuthenticationHeader(visaAliasApiOptions));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                client.DefaultRequestHeaders.Add("keyId", visaAliasApiOptions.KeyId);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                HttpClientHandler httpClientHandler = new();
                httpClientHandler.ClientCertificates.Add(GetCertificate(services, visaAliasApiOptions));
                return httpClientHandler;
            });
        }

        private static string GetBasicAuthenticationHeader(VisaAliasApiOptions visaAliasApiOptions)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes($"{visaAliasApiOptions.UserID}:{visaAliasApiOptions.Password}"));
        }

        private static X509Certificate2 GetCertificate(IServiceCollection services, VisaAliasApiOptions visaAliasApiOptions)
        {
            using X509Store store = new(StoreLocation.LocalMachine);
            store.Open(OpenFlags.OpenExistingOnly);
            X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindByThumbprint, visaAliasApiOptions.AuthorizationCertificateThumbprint, visaAliasApiOptions.AllowInvalidCertificate);
            X509Certificate2 certificate = certs.OfType<X509Certificate2>().FirstOrDefault();
            if (certificate is null)
            {
                ServiceProvider serviceProvider = services.BuildServiceProvider();
                IProblemDetailsHelper _problemDetailsHelper = serviceProvider.GetService<IProblemDetailsHelper>();
                throw new ApiProblemDetailsException(_problemDetailsHelper.GetWrongCertificateProblemDetails());
            }
            return certificate;
        }
    }
}
