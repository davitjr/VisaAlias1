using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.DTOs.VisaAlias;
using AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Infrastructure.Services.AcbaVisaAlias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Action = AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias.Action;

namespace AcbaVisaAliasApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VisaAliasController : ControllerBase
    {
        public VisaAliasController(IVisaAliasService AcbaVisaAliasService)
        {
            _AcbaVisaAliasService = AcbaVisaAliasService;
        }

        private readonly IVisaAliasService _AcbaVisaAliasService;

        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public async Task<IActionResult> TestCredentials()
        {
            TestCredentialsResponse response = await _AcbaVisaAliasService.TestCredentials();
            return Ok(response);
        }

        /// <summary>
        /// Get Visa Alias Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="404">The origin server did not find a current representation for the target resource or is not willing to disclose that one exists.</response>
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(GetAliasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetVisaAliasAsync([FromBody] GetAliasRequest request)
        {
            GetAliasResponse response = await _AcbaVisaAliasService.GetVisaAliasAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Create Visa Alias Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreateAliasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateVisaAliasAsync([FromBody] CreateAliasRequest request)
        {
            CreateAliasResponse response = await _AcbaVisaAliasService.CreateVisaAliasAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Update Visa Alias Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).</response>
        /// <response code="404">The origin server did not find a current representation for the target resource or is not willing to disclose that one exists.</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(UpdateAliasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateVisaAliasAsync([FromBody] UpdateAliasRequest request)
        {
            UpdateAliasResponse response = await _AcbaVisaAliasService.UpdateVisaAliasAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete Visa Alias Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).</response>
        /// <response code="404">The origin server did not find a current representation for the target resource or is not willing to disclose that one exists.</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DeleteAliasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteVisaAliasAsync([FromBody] DeleteAliasRequest request)
        {
            DeleteAliasResponse response = await _AcbaVisaAliasService.DeleteVisaAliasAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Get Visa Alias Report Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="202">The request has been accepted for processing, but the processing has not been completed. The request might or might not eventually be acted upon, as it might be disallowed when processing actually takes place.</response>
        /// <response code="400">The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(GetAliasReportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetVisaAliasReportAsync([FromBody] GetAliasReportRequest request)
        {
            GetAliasReportResponse response = await _AcbaVisaAliasService.GetVisaAliasReportAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Generate Visa Alias Report Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="202">The request has been accepted for processing, but the processing has not been completed. The request might or might not eventually be acted upon, as it might be disallowed when processing actually takes place.</response>
        /// <response code="400">The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).</response>
        /// <response code="404">The origin server did not find a current representation for the target resource or is not willing to disclose that one exists.</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(GenerateAliasReportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GenerateVisaAliasReportAsync([FromBody] GenerateAliasReportRequest request)
        {
            GenerateAliasReportResponse response = await _AcbaVisaAliasService.GenerateVisaAliasReportAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Resolve Visa Alias Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).</response>
        /// <response code="401">The request has not been applied because it lacks valid authentication credentials for the target resource.</response>
        /// <response code="404">The origin server did not find a current representation for the target resource or is not willing to disclose that one exists.</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResolveAliasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ResolveVisaAliasAsync([FromBody] ResolveAliasRequest request)
        {
            ResolveAliasResponse response = await _AcbaVisaAliasService.ResolveVisaAliasAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Resolve Visa Alias Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).</response>
        /// <response code="401">The request has not been applied because it lacks valid authentication credentials for the target resource.</response>
        /// <response code="404">The origin server did not find a current representation for the target resource or is not willing to disclose that one exists.</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResolveAliasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CheckVisaAliasAsync([FromBody] ResolveAliasRequest request)
        {
            ResolveAliasResponse response = await _AcbaVisaAliasService.ResolveVisaAliasAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Get Visa Alias history
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        [HttpPost]
        [ProducesResponseType(typeof(List<VisaAliasActionHistoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetVisaAliasHistory([FromBody] VisaAliasHistoryRequest request)
        {
            List<VisaAliasActionHistoryResponse> response = await _AcbaVisaAliasService.GetVisaAliasHistory(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete Visa Alias Api
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).</response>
        /// <response code="404">The origin server did not find a current representation for the target resource or is not willing to disclose that one exists.</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DeleteAliasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteVisaAliasWithCardAsync([FromBody] DeleteVisaAliasWithCardRequest deleteVisaAliasWithCardRequest)
        {
            DeleteVisaAliasRequest deleteVisaAliasRequest = await _AcbaVisaAliasService.GetVisaAliasForDeleteWithCard(deleteVisaAliasWithCardRequest);

            if (!string.IsNullOrEmpty(deleteVisaAliasRequest.Guid))
            {
                DeleteAliasRequest request = new(deleteVisaAliasRequest.Guid, deleteVisaAliasRequest.Alias, deleteVisaAliasWithCardRequest.SetNumber);
                await _AcbaVisaAliasService.DeleteVisaAliasAsync(request);
            }

            return Ok();
        }

        /// <summary>
        /// Get Visa Alias history with card number
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        [HttpPost]
        [ProducesResponseType(typeof(VisaAliasActionHistoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetVisaAliasHistoryWithCardAsync([FromBody] VisaAliasHistoryWithCard request)
        {
            VisaAliasActionHistoryResponse response = await _AcbaVisaAliasService.GetVisaAliasHistoryWithCardAsync(request);

            return Ok(response);
        }

        /// <summary>
        /// Update Or Delete Visa Alias based on customer main mobile phone changes
        /// </summary>
        /// <returns></returns>
        /// <response code="200">The request has succeeded.</response>
        /// <response code="400">The server cannot or will not process the request due to something that is perceived to be a client error (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).</response>
        /// <response code="404">The origin server did not find a current representation for the target resource or is not willing to disclose that one exists.</response>
        /// <response code="500">The server encountered an unexpected condition that prevented it from fulfilling the request.</response>     
        /// <response code="503">The server is currently unable to handle the request due to a temporary overload or scheduled maintenance, which will likely be alleviated after some delay.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DeleteAliasResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> ModifyVisaAliasByPhoneNumberAsync([FromBody] VisaAliasChangeRequest request)
        {
            switch (request.Action)
            {
                case Action.Update:
                    {
                        UpdateAliasResponse response = await _AcbaVisaAliasService.UpdateVisaAliasByPhoneNumberAsync(request);
                        return Ok(response);
                    }
                case Action.Delete:
                    {
                        DeleteAliasResponse response = await _AcbaVisaAliasService.DeleteVisaAliasByPhoneNumberAsync(request);
                        return Ok(response);
                    }
                default:
                    return BadRequest();
            }
        }
    }
}
