using Microsoft.AspNetCore.Components.Authorization;
using Restaurant.Blazor.Services.Interfaces;
using System.Net.Http.Headers;

namespace Restaurant.Blazor.Services.Implementations
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;

        public ApiService(IHttpClientFactory httpClientFactory, ILogger<ApiService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("WebAPI");
            _logger = logger;
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Call API failed");
                throw;
            }
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest payload)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, payload);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TResponse>();
                }
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Call API failed");
                throw;
            }
        }

        public async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest payload)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(endpoint, payload);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TResponse>();
                }
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Call API failed");
                throw;
            }
        }

        public async Task<TResponse?> DeleteAsync<TResponse>(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TResponse>();
                }
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Call API failed");
                throw;
            }
        }
    }
}
