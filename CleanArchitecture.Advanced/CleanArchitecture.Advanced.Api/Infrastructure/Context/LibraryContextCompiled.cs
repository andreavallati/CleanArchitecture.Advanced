using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Advanced.Common.Application.Requests;
using CleanArchitecture.Advanced.Api.Domain.Entities;

namespace CleanArchitecture.Advanced.Api.Infrastructure.Context
{
    public partial class LibraryContext : DbContext
    {
        public async Task<List<Library>> GetFilteredLibrariesAsync(SearchRequest filterRequest)
        {
            var result = new List<Library>();

            await foreach (var item in _getFilteredLibrariesAsync(this, filterRequest.Name, filterRequest.Address))
            {
                result.Add(item);
            }

            return result;
        }

        private static readonly Func<LibraryContext, string?, string?, IAsyncEnumerable<Library>> _getFilteredLibrariesAsync =
            EF.CompileAsyncQuery(
                (LibraryContext context, string? name, string? address) =>
                context.Libraries
                       .Where(l => l.Name == name && l.Address == address)
                );
    }
}
