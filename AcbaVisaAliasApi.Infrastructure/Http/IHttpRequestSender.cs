using System.IO;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.Http
{
    public interface IHttpRequestSender
    {
        Task<Stream> SendPostRequest(string requestUri, object content);
        Task<Stream> SendGetRequest(string requestUri);
    }
}