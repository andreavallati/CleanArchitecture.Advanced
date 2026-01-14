using CleanArchitecture.Advanced.Client.Domain.Models.Base;

namespace CleanArchitecture.Advanced.Client.Domain.Models
{
    public class AuthorModel : ModelBase
    {
        public long AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
