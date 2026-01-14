using CleanArchitecture.Advanced.Common.Application.Responses;
using CleanArchitecture.Advanced.Common.Constants;
using CleanArchitecture.Advanced.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CleanArchitecture.Advanced.Api.Infrastructure.Middleware
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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Pass control to the next middleware in the pipeline
                await _next(httpContext);
            }
            catch (ValidatorException ex)
            {
                // Handle FluentValidation exceptions
                _logger.LogError("Validation error: {validationErrorMessage}", ex.Message);

                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                _logger.LogError("Something went wrong: {errorMessage}", ex.Message);

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidatorException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // Create the validation error response using the FluentValidation exception data
            var validationErrorResponse = new ValidationErrorResponse
            {
                Message = CommonConstants.GenericValidationMessage,
                Errors = ex.ValidationErrors
            };

            // Serialize and return the validation error response
            var result = JsonConvert.SerializeObject(validationErrorResponse);
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                Message = CommonConstants.GenericErrorMessage,
                Details = exception.Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
