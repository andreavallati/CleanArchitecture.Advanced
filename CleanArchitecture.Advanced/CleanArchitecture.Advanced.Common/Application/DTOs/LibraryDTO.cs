using CleanArchitecture.Advanced.Common.Application.DTOs.Base;

namespace CleanArchitecture.Advanced.Common.Application.DTOs
{
    public class LibraryDTO : DTOBase
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public ICollection<BookDTO> Books { get; set; }
    }
}
