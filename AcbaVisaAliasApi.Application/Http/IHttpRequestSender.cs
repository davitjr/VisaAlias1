using AcbaVisaAliasApi.Infrastructure;
using System.IO;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Application.Http
{
    public interface IHttpRequestSender
    {
        Task<Stream> SendPostRequest(string requestUri, object content, VisaAliasAction visaAliasAction, string guId);
        Task<Stream> SendGetRequest(string requestUri, VisaAliasAction visaAliasAction, string guId);
    }
}