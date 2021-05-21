using System.Net.Http;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Application.Http
{
    public interface IHttpResponseHandler
    {
        Task HandleHttpResponse(HttpResponseMessage httpResponseMessage);
    }
}
