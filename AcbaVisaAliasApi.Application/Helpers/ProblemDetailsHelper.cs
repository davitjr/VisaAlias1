using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net;

namespace AcbaVisaAliasApi.Application.Helpers
{
    public class ProblemDetailsHelper : IProblemDetailsHelper
    {
        private IStringLocalizer<SharedResource> _localizer;
        private ProblemDetails ProblemDetails { get; set; }
        public ProblemDetailsHelper(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public ProblemDetails GetGlobalProblemDetails()
        {
            ProblemDetails = new ProblemDetails { Type = _localizer["StatusCode500RfcType"], Title = _localizer["SomethingWentWrongTitle"], Detail = _localizer["SomethingWentWrongMessage"], Status = StatusCodes.Status500InternalServerError };
            return ProblemDetails;
        }
        public ProblemDetails GetWrongCertificateProblemDetails()
        {
            ProblemDetails = new ProblemDetails { Type = _localizer["StatusCode400RfcType"], Title = _localizer["InvalidCertificateTitle"], Detail = _localizer["InvalidCertificateMessage"], Status = StatusCodes.Status400BadRequest };
            return ProblemDetails;
        }
        public ProblemDetails SetAliasErrorProblemDetail(AliasErrorResponse aliasErrorResponse, HttpStatusCode httpStatusCode)
        {
            ProblemDetails = new ProblemDetails { Title = aliasErrorResponse.Reason, Detail = aliasErrorResponse.Message };
            switch (httpStatusCode)
            {
                case HttpStatusCode.BadRequest:
                    {
                        ProblemDetails.Type = _localizer["StatusCode400RfcType"];
                        ProblemDetails.Status = StatusCodes.Status400BadRequest;
                    }
                    break;
                case HttpStatusCode.NotFound:
                    {
                        ProblemDetails.Type = _localizer["StatusCode404RfcType"];
                        ProblemDetails.Status = StatusCodes.Status404NotFound;
                    }
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    {
                        ProblemDetails.Type = _localizer["StatusCode503RfcType"];
                        ProblemDetails.Status = StatusCodes.Status503ServiceUnavailable;
                    }
                    break;
                case HttpStatusCode.InternalServerError:
                    {
                        ProblemDetails.Type = _localizer["StatusCode500RfcType"];
                        ProblemDetails.Status = StatusCodes.Status500InternalServerError;
                    }
                    break;
            }
            return ProblemDetails;
        }
    }
}
