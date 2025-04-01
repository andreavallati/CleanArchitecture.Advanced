using CleanArchitecture.Advanced.Client.Domain.Models.Base;

namespace CleanArchitecture.Advanced.Client.Domain.Models
{
    public class BookModel : ModelBase
    {
        public string? Title { get; set; }
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; } = true;

        // Just reference the LibraryId instead of the entire LibraryDTO
        public long LibraryId { get; set; }

        // Reference the Author
        public long AuthorId { get; set; }
        public AuthorModel Author { get; set; }

        public ICollection<LoanModel> Loans { get; set; }
    }
}
