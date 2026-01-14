using CleanArchitecture.Advanced.Common.Application.DTOs;
using CleanArchitecture.Advanced.Common.Application.Requests;

namespace CleanArchitecture.Advanced.Api.Application.Interfaces.Services
{
    public interface ILibraryService
    {
        Task<IEnumerable<LibraryDTO>> GetAllLibrariesAsync();
        Task<IEnumerable<LibraryDTO>> GetFilteredLibrariesAsync(SearchRequest filterRequest);
        Task<IEnumerable<LibraryDTO>> GetWhereLibrariesAsync(GetWhereRequest getWhereRequest);
        Task<LibraryDTO> GetLibraryByIdAsync(long libraryId);
        Task<LibraryDTO> FirstLibraryAsync();
        Task<LibraryDTO> FirstLibraryAsync(FirstEntityRequest firstEntityRequest);
        Task<IEnumerable<string>> SelectLibrariesNamesAsync();
        Task<bool> InsertLibraryAsync(LibraryDTO library);
        Task<bool> UpdateLibraryAsync(LibraryDTO library);
        Task<bool> DeleteLibraryAsync(long libraryId);
    }
}
