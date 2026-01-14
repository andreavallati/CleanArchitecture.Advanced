using CleanArchitecture.Advanced.Common.Application.DTOs.Base;

namespace CleanArchitecture.Advanced.Common.Application.DTOs
{
    public class LoanDTO : DTOBase
    {
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        // Reference only the BookId instead of the full BookDTO
        public long BookId { get; set; }

        // Reference only the BorrowerId instead of the full BorrowerDTO
        public long BorrowerId { get; set; }
    }
}
