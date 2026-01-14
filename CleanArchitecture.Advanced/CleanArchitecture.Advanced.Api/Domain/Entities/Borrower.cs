using CleanArchitecture.Advanced.Api.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Advanced.Api.Domain.Entities
{
    public class Borrower : EntityBase
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        // One-to-many relationship: One borrower can have many loans
        public ICollection<Loan> Loans { get; set; }
    }
}
