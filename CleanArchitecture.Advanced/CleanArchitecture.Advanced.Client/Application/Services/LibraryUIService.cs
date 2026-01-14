using AutoMapper;
using CleanArchitecture.Advanced.Client.Application.Interfaces.Connectors;
using CleanArchitecture.Advanced.Client.Application.Interfaces.Services;
using CleanArchitecture.Advanced.Client.Domain.Models;
using CleanArchitecture.Advanced.Common.Application.DTOs;
using CleanArchitecture.Advanced.Common.Application.Requests;
using CleanArchitecture.Advanced.Common.Application.Responses;

namespace CleanArchitecture.Advanced.Client.Application.Services
{
    public class LibraryUIService : ILibraryUIService
    {
        private readonly IMapper _mapper;
        private readonly ILibraryApiConnector _libraryApiConnector;

        public LibraryUIService(ILibraryApiConnector libraryApiConnector, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _libraryApiConnector = libraryApiConnector ?? throw new ArgumentNullException(nameof(libraryApiConnector));
        }

        public async Task<ApiResponseItems<LibraryModel>> GetAllLibrariesAsync()
        {
            return await ProcessItemsResponseAsync<LibraryModel, LibraryDTO>(async () => await _libraryApiConnector.GetModels<LibraryDTO>("libraries"));
        }

        public async Task<ApiResponseItems<LibraryModel>> GetFilteredLibrariesAsync(SearchRequest filterRequest)
        {
            return await ProcessItemsResponseAsync<LibraryModel, LibraryDTO>(async () => await _libraryApiConnector.PostModels<LibraryDTO, SearchRequest>("libraries/search", filterRequest));
        }

        public async Task<ApiResponseItems<LibraryModel>> GetWhereLibrariesAsync(GetWhereRequest getWhereRequest)
        {
            return await ProcessItemsResponseAsync<LibraryModel, LibraryDTO>(async () => await _libraryApiConnector.PostModels<LibraryDTO, GetWhereRequest>("libraries/get-where", getWhereRequest));
        }

        public async Task<ApiResponseItem<LibraryModel>> GetLibraryByIdAsync(long libraryId)
        {
            return await ProcessItemResponseAsync<LibraryModel, LibraryDTO>(async () => await _libraryApiConnector.GetModel<LibraryDTO>($"libraries/{libraryId}"));
        }

        public async Task<ApiResponseItem<LibraryModel>> FirstLibraryAsync()
        {
            return await ProcessItemResponseAsync<LibraryModel, LibraryDTO>(async () => await _libraryApiConnector.GetModel<LibraryDTO>("libraries/first"));
        }

        public async Task<ApiResponseItem<LibraryModel>> FirstLibraryAsync(FirstEntityRequest firstEntityRequest)
        {
            return await ProcessItemResponseAsync<LibraryModel, LibraryDTO>(async () => await _libraryApiConnector.PostModel<LibraryDTO, FirstEntityRequest>($"libraries/first", firstEntityRequest));
        }

        public async Task<ApiResponseItems<string>> SelectLibrariesNamesAsync()
        {
            return await ProcessItemsResponseAsync<string, string>(async () => await _libraryApiConnector.GetModels<string>("libraries/select"));
        }

        public Task<ApiResponseItem<bool>> InsertLibraryAsync(LibraryModel library)
        {
            var model = _mapper.Map<LibraryDTO>(library);
            return ProcessItemResponseAsync<bool, bool>(async () => await _libraryApiConnector.PostModel<bool, LibraryDTO>("libraries", model));
        }

        public async Task<ApiResponseItem<bool>> UpdateLibraryAsync(LibraryModel library)
        {
            var model = _mapper.Map<LibraryDTO>(library);
            return await ProcessItemResponseAsync<bool, bool>(async () => await _libraryApiConnector.PutModel<bool, LibraryDTO>("libraries", model));
        }

        public async Task<ApiResponseItem<bool>> DeleteLibraryAsync(long libraryId)
        {
            return await ProcessItemResponseAsync<bool, bool>(async () => await _libraryApiConnector.DeleteModel<bool>($"libraries/{libraryId}"));
        }

        private async Task<ApiResponseItem<TResult>> ProcessItemResponseAsync<TResult, TSource>(Func<Task<ApiResponseItem<TSource>>> apiCall)
        {
            try
            {
                var response = await apiCall();

                if (response.IsSuccess)
                {
                    var result = _mapper.Map<TResult>(response.Item);
                    return ApiResponseItem<TResult>.Success(result, response.StatusCode);
                }

                return ApiResponseItem<TResult>.Failure(response.ErrorMessage, response.StatusCode, response.ValidationErrors);
            }
            catch (Exception ex)
            {
                return ApiResponseItem<TResult>.Failure(ex, ex.StackTrace);
            }
        }

        private async Task<ApiResponseItems<TResult>> ProcessItemsResponseAsync<TResult, TSource>(Func<Task<ApiResponseItems<TSource>>> apiCall)
        {
            try
            {
                var response = await apiCall();

                if (response.IsSuccess)
                {
                    var result = _mapper.Map<IEnumerable<TResult>>(response.Items);
                    return ApiResponseItems<TResult>.Success(result, response.StatusCode);
                }

                return ApiResponseItems<TResult>.Failure(response.ErrorMessage, response.StatusCode, response.ValidationErrors);
            }
            catch (Exception ex)
            {
                return ApiResponseItems<TResult>.Failure(ex, ex.StackTrace);
            }
        }
    }
}
