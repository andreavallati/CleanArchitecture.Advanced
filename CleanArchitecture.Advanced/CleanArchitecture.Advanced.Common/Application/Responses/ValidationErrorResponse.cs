namespace CleanArchitecture.Advanced.Common.Application.Responses
{
    public class ValidationErrorResponse
    {
        public string? Message { get; set; }
        public IEnumerable<string> Errors { get; set; } = [];
    }
}
