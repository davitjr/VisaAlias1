using AcbaVisaAliasApi.Application.Helpers;
using AcbaVisaAliasApi.Application.Settings;
using AcbaVisaAliasApi.Infrastructure;
using Microsoft.AspNetCore.Http;
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
            ICryptographyHelper cryptographyHelper, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(VisaHttpRequestSender));
            _httpResponseHandlers = httpResponseHandlers;
            _cryptographyHelper = cryptographyHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICryptographyHelper _cryptographyHelper;
        private readonly IEnumerable<IHttpResponseHandler> _httpResponseHandlers;
        private readonly HttpClient _httpClient;

        public async Task<Stream> SendPostRequest(string requestUri, object content)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(requestUri, new StringContent(
                _cryptographyHelper.GetEncryptedPayload(JsonSerializer.Serialize(content, DefaultJsonSettings.Settings)),
                Encoding.UTF8, MediaTypeNames.Application.Json));
            
            //TODO: Add Table To log History     await httpResponseMessage.Content.ReadAsStringAsync(); httpResponseMessage.StatusCode; requestUri
            _httpContextAccessor.HttpContext.Response.Headers.Add("X-CORRELATION-ID", httpResponseMessage.Headers.GetValues("X-CORRELATION-ID").FirstOrDefault());
            return await ResponseHandlerAsync(httpResponseMessage);
        }

        public async Task<Stream> SendGetRequest(string requestUri)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(requestUri);
            //TODO: Add Table To log History     await httpResponseMessage.Content.ReadAsStringAsync(); httpResponseMessage.StatusCode; requestUri
            _httpContextAccessor.HttpContext.Response.Headers.Add("X-CORRELATION-ID", httpResponseMessage.Headers.GetValues("X-CORRELATION-ID").FirstOrDefault());
            return await ResponseHandlerAsync(httpResponseMessage);
        }

        private async Task<Stream> ResponseHandlerAsync(HttpResponseMessage httpResponseMessage)
        {
            foreach (IHttpResponseHandler httpResponseHandler in _httpResponseHandlers)
            {
                await httpResponseHandler.HandleHttpResponse(httpResponseMessage);
            }
            return await httpResponseMessage.Content.ReadAsStreamAsync();
        }
    }
}
