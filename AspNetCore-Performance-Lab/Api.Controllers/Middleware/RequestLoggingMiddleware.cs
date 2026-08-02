namespace Api.Controllers.Middleware
{
    public class RequestLoggingMiddleware
    {
        public readonly RequestDelegate _next;
        public readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation(
                "Incoming Request : {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            await _next(context);

            _logger.LogInformation(
                "Response Status: {StatusCode}",
                context.Response.StatusCode);
        }
    }
}
