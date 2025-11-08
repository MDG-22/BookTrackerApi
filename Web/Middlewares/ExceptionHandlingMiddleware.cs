using Domain.Exceptions;
namespace Web.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string errorCode = "UNEXPECTED_ERROR";
            string message = ex.Message;

            logger.LogError(ex, "Error capturado en middleware global");

            switch (ex)
            {
                case NotFoundException notFound:
                    statusCode = StatusCodes.Status404NotFound;
                    errorCode = !string.IsNullOrEmpty(notFound.ErrorCode) ? notFound.ErrorCode : "NOT_FOUND";
                    break;

                case AppValidationException validation:
                    statusCode = StatusCodes.Status400BadRequest;
                    errorCode = !string.IsNullOrEmpty(validation.ErrorCode) ? validation.ErrorCode : "VALIDATION_ERROR";
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    errorCode = "UNAUTHORIZED";
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var response = new
            {
                error = new
                {
                    code = errorCode,
                    message,
                    status = statusCode
                }
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
