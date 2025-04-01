using CleanArchitecture.Advanced.Api.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Advanced.Api.Domain.Entities
{
    public class Author : EntityBase
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        // One-to-many relationship: One author can write many books
        public ICollection<Book> Books { get; set; }
    }
}
