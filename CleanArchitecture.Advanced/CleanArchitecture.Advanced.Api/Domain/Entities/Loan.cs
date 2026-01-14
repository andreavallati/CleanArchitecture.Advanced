using CleanArchitecture.Advanced.Api.Domain.Entities.Base;

namespace CleanArchitecture.Advanced.Api.Domain.Entities
{
    public class Loan : EntityBase
    {
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        // Foreign key to Book
        public long BookId { get; set; }
        public Book Book { get; set; }

        // Foreign key to Borrower
        public long BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
    }
}
