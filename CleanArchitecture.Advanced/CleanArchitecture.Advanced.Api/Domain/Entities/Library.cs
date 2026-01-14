using CleanArchitecture.Advanced.Api.Domain.Entities.Base;

namespace CleanArchitecture.Advanced.Api.Domain.Entities
{
    public class Library : EntityBase
    {
        public string? Name { get; set; }
        public string? Address { get; set; }

        // One-to-many relationship: One library can have many books
        public ICollection<Book> Books { get; set; }
    }
}
