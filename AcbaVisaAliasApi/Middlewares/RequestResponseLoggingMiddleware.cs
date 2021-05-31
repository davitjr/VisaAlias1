using AcbaVisaAliasApi.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IO;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger, IOptions<VisaAliasApiOptions> VisaAliasOptions)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _VisaAliasApiOptions = VisaAliasOptions.Value;
        }

        private readonly VisaAliasApiOptions _VisaAliasApiOptions;
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            _logger.LogInformation(await FormatRequest(context.Request));
            context.Request.Body.Position = 0;
        }
        private async Task LogResponse(HttpContext context)
        {
            Stream originalBodyStream = context.Response.Body;
            await using MemoryStream responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            _logger.LogInformation(await FormatResponse(context, responseBody));
            await responseBody.CopyToAsync(originalBodyStream);
        }
        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using StringWriter textWriter = new();
            using StreamReader reader = new(stream);
            char[] readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            await using MemoryStream requestStream = _recyclableMemoryStreamManager.GetStream();
            await request.Body.CopyToAsync(requestStream);

            return $"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{request.Scheme} " +
                                   $"Host: {request.Host} " +
                                   $"Path: {request.Path} " +
                                   $"QueryString: {request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}";
        }
        private async Task<string> FormatResponse(HttpContext context, MemoryStream responseBody)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            string text = _VisaAliasApiOptions.EnableOkResponseLogging ? await new StreamReader(responseBody).ReadToEndAsync() : string.Empty;
            responseBody.Seek(0, SeekOrigin.Begin);
            context.Response.Headers.TryGetValue("X-CORRELATION-ID", out StringValues corralationId);

            return $"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"StatusCode:{context.Response.StatusCode} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text} " +
                                   $"X-CORRELATION-ID: {corralationId}";
        }
    }
}
