using CleanArchitecture.Advanced.Common.Application.DTOs.Base;

namespace CleanArchitecture.Advanced.Common.Application.DTOs
{
    public class BookDTO : DTOBase
    {
        public string? Title { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; } = true;

        // Just reference the LibraryId instead of the entire LibraryDTO
        public long LibraryId { get; set; }

        // Reference the Author
        public long AuthorId { get; set; }
        public AuthorDTO Author { get; set; }

        public ICollection<LoanDTO> Loans { get; set; }
    }
}
