using System.Text.Json;
using Egress.API.Models;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;
using Newtonsoft.Json;

namespace Egress.API.Middlewares;

public class GlobalExceptionMiddleware
{
    #region Constants
    private const string CONTENT_TYPE = "application/json";
    private const string TRACE_ID_NAME = "TraceId";
    #endregion
    
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    /// <summary>
    /// Exception interceptor method
    /// </summary>
    /// <param name="context">Http context</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException e)
        {
            var response = new GenericHttpResponse
            {
                Errors = e.Errors.Select(error => error.ErrorMessage),
                StatusCode = StatusCodes.Status400BadRequest,
                Data = default
            };

            _logger.LogError(e, $"[{context.Request.Method} {context.Request.Path}] {LoggerResource.DATA_VALIDATION_FAILURE}, {TRACE_ID_NAME}: {response.TraceId}");

            await BuildResponseAsync(context, response.StatusCode, JsonConvert.SerializeObject(response), CONTENT_TYPE);
        }
        catch (BusinessException e)
        {
            var response = new GenericHttpResponse
            {
                Errors = new List<string> { e.Message },
                StatusCode = StatusCodes.Status400BadRequest,
                Data = default
            };

            _logger.LogError(e, $"[{context.Request.Method} {context.Request.Path}] {LoggerResource.BUSINESS_FAILURE}: {e.Message}, {TRACE_ID_NAME}: {response.TraceId}");

            await BuildResponseAsync(context, response.StatusCode, JsonConvert.SerializeObject(response), CONTENT_TYPE);
        }
        catch (Exception e)
        {
            var response = new GenericHttpResponse
            {
                Errors = new List<string> { ErrorCodeResource.UNEXPECTED_ERROR_OCURRED },
                StatusCode = StatusCodes.Status400BadRequest,
                Data = default
            };

            _logger.LogError(e, $"[{context.Request.Method} {context.Request.Path}] {LoggerResource.UNEXPECTED_ERROR_OCURRED}, {TRACE_ID_NAME}: {response.TraceId}");

            await BuildResponseAsync(context, response.StatusCode, JsonConvert.SerializeObject(response), CONTENT_TYPE);
        }
    }
    /// <summary>
    /// Build HTTP response
    /// </summary>
    /// <param name="context">Context</param>
    /// <param name="statusCodes">Status code</param>
    /// <param name="body">Response body</param>
    /// <param name="contentType">Content type</param>
    private async Task BuildResponseAsync(HttpContext context, int statusCodes, string body, string contentType)
    {
        context.Response.Clear();
        context.Response.StatusCode = statusCodes;
        context.Response.ContentType = contentType;
        await context.Response.WriteAsync(body);
    }
}