using CleanArchitecture.Advanced.Client.Domain.Models.Base;

namespace CleanArchitecture.Advanced.Client.Domain.Models
{
    public class LoanModel : ModelBase
    {
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Reference only the BookId instead of the full BookModel
        public long BookId { get; set; }

        // Reference only the BorrowerId instead of the full BorrowerModel
        public long BorrowerId { get; set; }
    }
}
