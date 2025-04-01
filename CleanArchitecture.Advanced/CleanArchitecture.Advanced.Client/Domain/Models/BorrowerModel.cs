using CleanArchitecture.Advanced.Client.Domain.Models.Base;

namespace CleanArchitecture.Advanced.Client.Domain.Models
{
    public class BorrowerModel : ModelBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<LoanModel> Loans { get; set; }
    }
}
