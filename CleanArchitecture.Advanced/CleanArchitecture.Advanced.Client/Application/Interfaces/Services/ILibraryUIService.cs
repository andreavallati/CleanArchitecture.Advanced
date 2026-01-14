using CleanArchitecture.Advanced.Client.Domain.Models;
using CleanArchitecture.Advanced.Common.Application.Requests;
using CleanArchitecture.Advanced.Common.Application.Responses;

namespace CleanArchitecture.Advanced.Client.Application.Interfaces.Services
{
    public interface ILibraryUIService
    {
        Task<ApiResponseItems<LibraryModel>> GetAllLibrariesAsync();
        Task<ApiResponseItems<LibraryModel>> GetFilteredLibrariesAsync(SearchRequest filterRequest);
        Task<ApiResponseItems<LibraryModel>> GetWhereLibrariesAsync(GetWhereRequest getWhereRequest);
        Task<ApiResponseItem<LibraryModel>> GetLibraryByIdAsync(long libraryId);
        Task<ApiResponseItem<LibraryModel>> FirstLibraryAsync();
        Task<ApiResponseItem<LibraryModel>> FirstLibraryAsync(FirstEntityRequest firstEntityRequest);
        Task<ApiResponseItems<string>> SelectLibrariesNamesAsync();
        Task<ApiResponseItem<bool>> InsertLibraryAsync(LibraryModel library);
        Task<ApiResponseItem<bool>> UpdateLibraryAsync(LibraryModel library);
        Task<ApiResponseItem<bool>> DeleteLibraryAsync(long libraryId);
    }
}
