using System.Text;
using System.Text.Json;
using WEB_253503_Soroka.UI.Services.GenreService;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;

namespace WEB_253503_Soroka.UI.ApiServices;

public class ApiGenreService : IGenreService
{
    private HttpClient _httpClient;
    private ILogger<ApiGenreService> _logger;
    private JsonSerializerOptions _serializerOptions;
    
    public ApiGenreService(HttpClient httpClient, ILogger<ApiGenreService> logger)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _logger = logger;
    }
    
    public async Task<ResponseData<List<Genre>>> GetGenreListAsync()
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}genres");
        
        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
        
        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<List<Genre>>>(_serializerOptions);
            }
            catch(JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<List<Genre>>.Error($"Ошибка: {ex.Message}");
            }
        }
        
        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        return ResponseData<List<Genre>>.Error(
            $"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        
    }
}