using AcbaVisaAliasApi.Application.Helpers;
using AcbaVisaAliasApi.Application.Settings;
using AcbaVisaAliasApi.Infrastructure;
using AcbaVisaAliasApi.Infrastructure.DBManager;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Application.Http
{
    public class VisaHttpRequestSender : IHttpRequestSender
    {
        public VisaHttpRequestSender(IHttpClientFactory httpClientFactory, IEnumerable<IHttpResponseHandler> httpResponseHandlers,
            ICryptographyHelper cryptographyHelper)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(VisaHttpRequestSender));
            _httpResponseHandlers = httpResponseHandlers;
            _cryptographyHelper = cryptographyHelper;
        }

        private readonly ICryptographyHelper _cryptographyHelper;
        private readonly IEnumerable<IHttpResponseHandler> _httpResponseHandlers;
        private readonly HttpClient _httpClient;

        public async Task<Stream> SendPostRequest(string requestUri, object content, VisaAliasAction visaAliasAction, string guId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(requestUri, new StringContent(
                _cryptographyHelper.GetEncryptedPayload(JsonSerializer.Serialize(content, DefaultJsonSettings.Settings)),
                Encoding.UTF8, MediaTypeNames.Application.Json));             

            return await ResponseHandlerAsync(httpResponseMessage, visaAliasAction, guId);
        }

        public async Task<Stream> SendGetRequest(string requestUri, VisaAliasAction visaAliasAction, string guId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(requestUri);
            return await ResponseHandlerAsync(httpResponseMessage, visaAliasAction, guId);
        }

        private async Task<Stream> ResponseHandlerAsync(HttpResponseMessage httpResponseMessage, VisaAliasAction visaAliasAction, string guId)
        {
            foreach (IHttpResponseHandler httpResponseHandler in _httpResponseHandlers)
            {
                await httpResponseHandler.HandleHttpResponse(httpResponseMessage);
            }
            string CorralationId = GetCorralationId(httpResponseMessage.Headers);           

            await VisaAliasDB.SaveCorrelationId(CorralationId, (int)visaAliasAction, guId);

            return await httpResponseMessage.Content.ReadAsStreamAsync();
        }

        private string GetCorralationId(HttpResponseHeaders header)
        {
            return header.GetValues("X-CORRELATION-ID").FirstOrDefault();
        }

       
    }
}
