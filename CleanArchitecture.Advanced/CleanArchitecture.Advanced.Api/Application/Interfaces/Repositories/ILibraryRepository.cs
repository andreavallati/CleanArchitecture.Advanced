using CleanArchitecture.Advanced.Api.Application.Interfaces.Repositories.Base;
using CleanArchitecture.Advanced.Api.Domain.Entities;
using CleanArchitecture.Advanced.Common.Application.Requests;

namespace CleanArchitecture.Advanced.Api.Application.Interfaces.Repositories
{
    public interface ILibraryRepository : IRepositoryBase<Library>
    {
        // Eventually define specific methods for LibraryRepository

        Task<List<Library>> GetFilteredLibrariesAsync(SearchRequest filterRequest);
    }
}
