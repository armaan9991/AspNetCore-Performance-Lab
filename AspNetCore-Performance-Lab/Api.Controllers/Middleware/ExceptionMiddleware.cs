using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Net;
using System.Text.Json;
using Api.Controllers.Responses;
using Api.Controllers.Exceptions;

namespace Api.Controllers.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ProductAlreadyExistsException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status409Conflict;

                await context.Response.WriteAsJsonAsync(
                    new ApiResponse<string>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
            }
            catch (ProductNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsJsonAsync(
                    new ApiResponse<string>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
            }
            catch (InvalidPriceException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(
                    new ApiResponse<string>
                    {
                        Success = false,
                        Message = ex.Message,
                        Data = null
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                var response = new ApiResponse<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
