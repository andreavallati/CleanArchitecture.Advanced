using CleanArchitecture.Advanced.Client.Application.Interfaces.Connectors.Base;
using CleanArchitecture.Advanced.Common.Application.Responses;
using CleanArchitecture.Advanced.Common.Constants;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CleanArchitecture.Advanced.Client.Infrastructure.Connectors.Base
{
    public abstract class ApiConnectorBase : IApiConnectorBase
    {
        protected readonly HttpClient _httpClient;

        protected ApiConnectorBase(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(CommonConstants.WebServiceEndpoint);
        }

        public async Task<ApiResponseItem<TResult>> GetModel<TResult>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await HandleHttpItemResponse<TResult>(response);
        }

        public async Task<ApiResponseItems<TResult>> GetModels<TResult>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await HandleHttpItemsResponse<TResult>(response);
        }

        public async Task<ApiResponseItem<TResult>> PostModel<TResult, TRequest>(string endpoint, TRequest request)
        {
            if (request is null)
            {
                throw new InvalidOperationException($"Request body can't be null.");
            }

            var jsonContent = BuildJsonContent(request);
            var response = await _httpClient.PostAsync(endpoint, jsonContent);
            return await HandleHttpItemResponse<TResult>(response);
        }

        public async Task<ApiResponseItems<TResult>> PostModels<TResult, TRequest>(string endpoint, TRequest request)
        {
            if (request is null)
            {
                throw new InvalidOperationException($"Request body can't be null.");
            }

            var jsonContent = BuildJsonContent(request);
            var response = await _httpClient.PostAsync(endpoint, jsonContent);
            return await HandleHttpItemsResponse<TResult>(response);
        }

        public async Task<ApiResponseItem<TResult>> PutModel<TResult, TRequest>(string endpoint, TRequest request)
        {
            if (request is null)
            {
                throw new InvalidOperationException($"Request body can't be null.");
            }

            var jsonContent = BuildJsonContent(request);
            var response = await _httpClient.PutAsync(endpoint, jsonContent);
            return await HandleHttpItemResponse<TResult>(response);
        }

        public async Task<ApiResponseItems<TResult>> PutModels<TResult, TRequest>(string endpoint, TRequest request)
        {
            if (request is null)
            {
                throw new InvalidOperationException($"Request body can't be null.");
            }

            var jsonContent = BuildJsonContent(request);
            var response = await _httpClient.PutAsync(endpoint, jsonContent);
            return await HandleHttpItemsResponse<TResult>(response);
        }

        public async Task<ApiResponseItem<TResult>> DeleteModel<TResult>(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return await HandleHttpItemResponse<TResult>(response);
        }

        public async Task<ApiResponseItems<TResult>> DeleteModels<TResult>(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return await HandleHttpItemsResponse<TResult>(response);
        }

        private static StringContent BuildJsonContent(object body)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var jsonString = JsonConvert.SerializeObject(body, Formatting.None, jsonSerializerSettings);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }

        private static async Task<ApiResponseItem<TResult>> HandleHttpItemResponse<TResult>(HttpResponseMessage response)
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<TResult>(jsonResponse) ?? default!;
                    return ApiResponseItem<TResult>.Success(result, response.StatusCode);
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var errorObject = JsonConvert.DeserializeObject<ValidationErrorResponse>(errorContent) ?? default!;
                    return ApiResponseItem<TResult>.Failure(CommonConstants.GenericValidationMessage, response.StatusCode, errorObject.Errors);
                }

                var failureContent = await response.Content.ReadAsStringAsync();
                return ApiResponseItem<TResult>.Failure(CommonConstants.GenericErrorMessage, default!, failureContent, response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                return ApiResponseItem<TResult>.Failure(ex.Message, ex, ex.StackTrace, ex.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponseItem<TResult>.Failure(ex.Message, ex, ex.StackTrace, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task<ApiResponseItems<TResult>> HandleHttpItemsResponse<TResult>(HttpResponseMessage response)
        {
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<IEnumerable<TResult>>(jsonResponse) ?? default!;
                    return ApiResponseItems<TResult>.Success(result, response.StatusCode);
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var errorObject = JsonConvert.DeserializeObject<ValidationErrorResponse>(errorContent) ?? default!;
                    return ApiResponseItems<TResult>.Failure(CommonConstants.GenericValidationMessage, response.StatusCode, errorObject.Errors);
                }

                var failureContent = await response.Content.ReadAsStringAsync();
                return ApiResponseItems<TResult>.Failure(CommonConstants.GenericErrorMessage, default!, failureContent, response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                return ApiResponseItems<TResult>.Failure(ex.Message, ex, ex.StackTrace, ex.StatusCode);
            }
            catch (Exception ex)
            {
                return ApiResponseItems<TResult>.Failure(ex.Message, ex, ex.StackTrace, HttpStatusCode.InternalServerError);
            }
        }
    }
}
