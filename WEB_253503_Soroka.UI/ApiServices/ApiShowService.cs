using System.Text;
using System.Text.Json;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.ApiServices;

public class ApiShowService : IShowService
{
    private HttpClient _httpClient;
    private ILogger<ApiShowService> _logger;
    private string _pageSize;
    private JsonSerializerOptions _serializerOptions;
    
    public ApiShowService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiShowService> logger)
    {
        _httpClient = httpClient;
        _pageSize = configuration.GetSection("ItemsPerPage").Value;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _logger = logger;
    }
    
    public async Task<ResponseData<ListModel<Show>>> GetShowListAsync(string? genreNormalizedName, int pageNo = 1)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}shows");

        if (genreNormalizedName != null)
        {
            urlString.Append($"/{genreNormalizedName}");
        }

        if (pageNo > 1)
        {
            urlString.Append(QueryString.Create("pageNo", pageNo.ToString()));
            // urlString.Append($"?pageNo={pageNo}");
        }

        if (!_pageSize.Equals("3"))
        {
            urlString.Append(QueryString.Create("pageSize", _pageSize));
        }

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
        
        if(response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Show>>>(_serializerOptions);
            }
            catch(JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<ListModel<Show>>.Error($"Ошибка: {ex.Message}");
            }
        }
        
        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        return ResponseData<ListModel<Show>>.Error(
            $"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
    }

    public Task<ResponseData<Show>> GetShowByIdAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateShowAsync(int id, Show show, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }

    public Task DeleteShowAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task CreateShowAsync(Show show, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }
}