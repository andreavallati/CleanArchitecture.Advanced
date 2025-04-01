using System.Net;

namespace CleanArchitecture.Advanced.Common.Application.Responses.Base
{
    public abstract class ApiResponseBase
    {
        public string? ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public string? StackTrace { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
        public IEnumerable<string> ValidationErrors { get; set; } = []; // Just for Fluent Validation errors

        public bool IsSuccess => StatusCode == HttpStatusCode.OK || StatusCode == HttpStatusCode.Created;
    }
}
