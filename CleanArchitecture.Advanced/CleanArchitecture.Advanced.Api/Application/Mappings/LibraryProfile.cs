using AutoMapper;
using CleanArchitecture.Advanced.Api.Domain.Entities;
using CleanArchitecture.Advanced.Common.Application.DTOs;

namespace CleanArchitecture.Advanced.Api.Application.Mappings
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<Library, LibraryDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Borrower, BorrowerDTO>().ReverseMap();
            CreateMap<Loan, LoanDTO>().ReverseMap();
            CreateMap<EventLog, EventLogDTO>().ReverseMap();
        }
    }
}
