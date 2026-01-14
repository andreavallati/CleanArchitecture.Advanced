using CleanArchitecture.Advanced.Api.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Advanced.Api.Domain.Entities
{
    public class Book : EntityBase
    {
        [Required]
        public string? Title { get; set; }

        public int PublishedYear { get; set; }

        // Default value for IsAvailable is true
        [Required]
        public bool IsAvailable { get; set; } = true;

        // Foreign key to Library
        public long LibraryId { get; set; }
        public Library Library { get; set; }

        // Foreign key to Author
        public long AuthorId { get; set; }
        public Author Author { get; set; }

        // One-to-many relationship with Loan (a book can have many loans)
        public ICollection<Loan> Loans { get; set; }
    }
}
