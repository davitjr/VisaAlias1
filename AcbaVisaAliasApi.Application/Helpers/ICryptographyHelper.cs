using System.IO;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Application.Helpers
{
    public interface ICryptographyHelper
    {
        string GetEncryptedPayload(string requestBody);
        Task<T> DecryptResponse<T>(Stream responseStream) where T : class;
    }
}