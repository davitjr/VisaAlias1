using System.Net.Http;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.Http
{
    public interface IHttpResponseHandler
    {
        Task HandleHttpResponse(HttpResponseMessage httpResponseMessage);
    }
}
