using AutoMapper;
using CleanArchitecture.Advanced.Client.Domain.Models;
using CleanArchitecture.Advanced.Common.Application.DTOs;

namespace CleanArchitecture.Advanced.Client.Application.Mappings
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<LibraryModel, LibraryDTO>().ReverseMap();
            CreateMap<BookModel, BookDTO>().ReverseMap();
            CreateMap<AuthorModel, AuthorDTO>().ReverseMap();
            CreateMap<BorrowerModel, BorrowerDTO>().ReverseMap();
            CreateMap<LoanModel, LoanDTO>().ReverseMap();
            CreateMap<EventLogModel, EventLogDTO>().ReverseMap();
        }
    }
}
