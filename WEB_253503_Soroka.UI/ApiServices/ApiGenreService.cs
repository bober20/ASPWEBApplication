using System.Text;
using System.Text.Json;
using WEB_253503_Soroka.UI.Services.GenreService;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;

namespace WEB_253503_Soroka.UI.ApiServices;

public class ApiGenreService : IGenreService
{
    private HttpClient _httpClient;
    private JsonSerializerOptions _serializerOptions;
    
    public ApiGenreService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
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
                return ResponseData<List<Genre>>.Error($"Ошибка: {ex.Message}");
            }
        }
        
        return ResponseData<List<Genre>>.Error(
            $"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        
    }
}