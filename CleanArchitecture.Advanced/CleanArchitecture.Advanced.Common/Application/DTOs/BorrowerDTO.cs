using CleanArchitecture.Advanced.Common.Application.DTOs.Base;

namespace CleanArchitecture.Advanced.Common.Application.DTOs
{
    public class BorrowerDTO : DTOBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<LoanDTO> Loans { get; set; }
    }
}
