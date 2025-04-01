using CleanArchitecture.Advanced.Common.Application.Responses.Base;
using System.Net;

namespace CleanArchitecture.Advanced.Common.Application.Responses
{
    public class ApiResponseItem<TModel> : ApiResponseBase
    {
        public TModel Item { get; set; } = default!;

        public static ApiResponseItem<TModel> Success(TModel item, HttpStatusCode? statusCode)
        {
            return new ApiResponseItem<TModel>
            {
                Item = item,
                StatusCode = statusCode,
            };
        }

        public static ApiResponseItem<TModel> Failure(TModel item,
                                                      string? errorMessage,
                                                      Exception ex,
                                                      string? stackTrace,
                                                      HttpStatusCode? statusCode,
                                                      IEnumerable<string> validationErrors)
        {
            return new ApiResponseItem<TModel>
            {
                Item = item,
                ErrorMessage = errorMessage,
                Exception = ex,
                StackTrace = stackTrace,
                StatusCode = statusCode,
                ValidationErrors = validationErrors
            };
        }

        public static ApiResponseItem<TModel> Failure(string? errorMessage,
                                                      Exception ex,
                                                      string? stackTrace,
                                                      HttpStatusCode? statusCode)
        {
            return Failure(default!, errorMessage, ex, stackTrace, statusCode, []);
        }

        public static ApiResponseItem<TModel> Failure(string? errorMessage,
                                                      Exception ex,
                                                      HttpStatusCode? statusCode)
        {
            return Failure(default!, errorMessage, ex, string.Empty, statusCode, []);
        }

        public static ApiResponseItem<TModel> Failure(string? errorMessage,
                                                      HttpStatusCode? statusCode,
                                                      IEnumerable<string> validationErrors)
        {
            return Failure(default!, errorMessage, default!, string.Empty, statusCode, validationErrors);
        }

        public static ApiResponseItem<TModel> Failure(string? errorMessage,
                                                      HttpStatusCode? statusCode)
        {
            return Failure(default!, errorMessage, default!, string.Empty, statusCode, []);
        }

        public static ApiResponseItem<TModel> Failure(Exception ex,
                                                      string? stackTrace)
        {
            return Failure(default!, string.Empty, ex, stackTrace, HttpStatusCode.InternalServerError, []);
        }
    }
}
