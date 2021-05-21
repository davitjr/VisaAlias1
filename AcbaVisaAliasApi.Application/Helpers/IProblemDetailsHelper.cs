using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AcbaVisaAliasApi.Application.Helpers
{
    public interface IProblemDetailsHelper
    {
        ProblemDetails GetGlobalProblemDetails();
        ProblemDetails GetWrongCertificateProblemDetails();
        ProblemDetails SetAliasErrorProblemDetail(AliasErrorResponse aliasErrorResponse, HttpStatusCode httpStatusCode);
    }
}