using CleanArchitecture.Advanced.Client.Domain.Models.Base;

namespace CleanArchitecture.Advanced.Client.Domain.Models
{
    public class LibraryModel : ModelBase
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public ICollection<BookModel> Books { get; set; }
    }
}
