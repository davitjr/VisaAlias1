using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.Helpers;
using AcbaVisaAliasApi.Application.Settings;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.Http
{
    public class HttpResponseStatusChecker : IHttpResponseHandler
    {
        public HttpResponseStatusChecker(IProblemDetailsHelper problemDetailsHelper, ICryptographyHelper cryptographyHelper, IOptions<VisaAliasApiOptions> VisaAliasOptions)
        {
            _problemDetailsHelper = problemDetailsHelper;
            _cryptographyHelper = cryptographyHelper;
            _VisaAliasApiOptions = VisaAliasOptions.Value;
        }

        private readonly VisaAliasApiOptions _VisaAliasApiOptions;
        private readonly IProblemDetailsHelper _problemDetailsHelper;
        private readonly ICryptographyHelper _cryptographyHelper;

        public async Task HandleHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                    break;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.ServiceUnavailable:
                case HttpStatusCode.NotFound:
                    {
                        AliasErrorResponse aliasErrorResponse = await _cryptographyHelper.DecryptResponse<AliasErrorResponse>(await httpResponseMessage.Content.ReadAsStreamAsync());
                        ProblemDetails validationProblemDetails = _problemDetailsHelper.SetAliasErrorProblemDetail(aliasErrorResponse, httpResponseMessage.StatusCode);
                        throw new ApiProblemDetailsException(validationProblemDetails);
                    }
                case HttpStatusCode.InternalServerError:
                    {
                        if (httpResponseMessage.RequestMessage.RequestUri.AbsolutePath == _VisaAliasApiOptions.GetAliasApi && httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                        {
                            break;
                        }
                        AliasErrorResponse aliasErrorResponse = await _cryptographyHelper.DecryptResponse<AliasErrorResponse>(await httpResponseMessage.Content.ReadAsStreamAsync());
                        ProblemDetails validationProblemDetails = _problemDetailsHelper.SetAliasErrorProblemDetail(aliasErrorResponse, httpResponseMessage.StatusCode);
                        throw new ApiProblemDetailsException(validationProblemDetails);
                    }
                default:
                    {
                        throw new ApiProblemDetailsException(_problemDetailsHelper.GetGlobalProblemDetails());
                    }
            }
        }
    }
}
