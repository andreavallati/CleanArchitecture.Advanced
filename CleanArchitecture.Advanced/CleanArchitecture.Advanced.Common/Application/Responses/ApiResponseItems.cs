using CleanArchitecture.Advanced.Common.Application.Responses.Base;
using System.Net;

namespace CleanArchitecture.Advanced.Common.Application.Responses
{
    public class ApiResponseItems<TModel> : ApiResponseBase
    {
        public IEnumerable<TModel> Items { get; set; } = [];

        public static ApiResponseItems<TModel> Success(IEnumerable<TModel> items, HttpStatusCode? statusCode)
        {
            return new ApiResponseItems<TModel>
            {
                Items = items,
                StatusCode = statusCode,
            };
        }

        public static ApiResponseItems<TModel> Failure(IEnumerable<TModel> items,
                                                       string? errorMessage,
                                                       Exception ex,
                                                       string? stackTrace,
                                                       HttpStatusCode? statusCode,
                                                       IEnumerable<string> validationErrors)
        {
            return new ApiResponseItems<TModel>
            {
                ErrorMessage = errorMessage,
                Exception = ex,
                StackTrace = stackTrace,
                StatusCode = statusCode,
                ValidationErrors = validationErrors
            };
        }

        public static ApiResponseItems<TModel> Failure(string? errorMessage,
                                                       Exception ex,
                                                       string? stackTrace,
                                                       HttpStatusCode? statusCode)
        {
            return Failure(default!, errorMessage, ex, stackTrace, statusCode, []);
        }

        public static ApiResponseItems<TModel> Failure(string? errorMessage,
                                                       Exception ex,
                                                       HttpStatusCode? statusCode)
        {
            return Failure(default!, errorMessage, ex, string.Empty, statusCode, []);
        }

        public static ApiResponseItems<TModel> Failure(string? errorMessage,
                                                       HttpStatusCode? statusCode,
                                                       IEnumerable<string> validationErrors)
        {
            return Failure(default!, errorMessage, default!, string.Empty, statusCode, validationErrors);
        }

        public static ApiResponseItems<TModel> Failure(string? errorMessage,
                                                       HttpStatusCode? statusCode)
        {
            return Failure(default!, errorMessage, default!, string.Empty, statusCode, []);
        }

        public static ApiResponseItems<TModel> Failure(Exception ex,
                                                       string? stackTrace)
        {
            return Failure(default!, string.Empty, ex, stackTrace, HttpStatusCode.InternalServerError, []);
        }
    }
}
