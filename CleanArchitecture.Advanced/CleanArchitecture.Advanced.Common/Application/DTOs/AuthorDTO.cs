using CleanArchitecture.Advanced.Common.Application.DTOs.Base;

namespace CleanArchitecture.Advanced.Common.Application.DTOs
{
    public class AuthorDTO : DTOBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
