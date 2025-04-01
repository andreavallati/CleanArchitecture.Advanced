using CleanArchitecture.Advanced.Api.Application.Interfaces.Caching;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Repositories;
using CleanArchitecture.Advanced.Api.Domain.Entities;
using CleanArchitecture.Advanced.Api.Infrastructure.Context;
using CleanArchitecture.Advanced.Api.Infrastructure.Repositories.Base;
using CleanArchitecture.Advanced.Common.Application.Requests;

namespace CleanArchitecture.Advanced.Api.Infrastructure.Repositories
{
    public class LibraryRepository : RepositoryBase<Library>, ILibraryRepository
    {
        public LibraryRepository(LibraryContext context, ICustomMemoryCache cache) : base(context, cache)
        {

        }

        // Eventually implement specific methods for LibraryRepository

        public async Task<List<Library>> GetFilteredLibrariesAsync(SearchRequest filterRequest)
        {
            return await _context.GetFilteredLibrariesAsync(filterRequest);
        }
    }
}
