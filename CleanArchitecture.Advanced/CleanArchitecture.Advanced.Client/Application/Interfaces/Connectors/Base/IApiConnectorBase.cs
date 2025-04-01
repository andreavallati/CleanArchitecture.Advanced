using CleanArchitecture.Advanced.Common.Application.Responses;

namespace CleanArchitecture.Advanced.Client.Application.Interfaces.Connectors.Base
{
    public interface IApiConnectorBase
    {
        Task<ApiResponseItem<TResult>> GetModel<TResult>(string endpoint);
        Task<ApiResponseItems<TResult>> GetModels<TResult>(string endpoint);
        Task<ApiResponseItem<TResult>> PostModel<TResult, TRequest>(string endpoint, TRequest request);
        Task<ApiResponseItems<TResult>> PostModels<TResult, TRequest>(string endpoint, TRequest request);
        Task<ApiResponseItem<TResult>> PutModel<TResult, TRequest>(string endpoint, TRequest request);
        Task<ApiResponseItems<TResult>> PutModels<TResult, TRequest>(string endpoint, TRequest request);
        Task<ApiResponseItem<TResult>> DeleteModel<TResult>(string endpoint);
        Task<ApiResponseItems<TResult>> DeleteModels<TResult>(string endpoint);
    }
}
