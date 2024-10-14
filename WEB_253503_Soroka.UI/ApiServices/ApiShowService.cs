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

    // public async Task<ResponseData<ListModel<Show>>> GetShowListAsync()
    // {
    //     var response = await _httpClient.GetAsync(new Uri($"{_httpClient.BaseAddress.AbsoluteUri}/shows"));
    //
    //     if (response.IsSuccessStatusCode)
    //     {
    //         try
    //         {
    //             return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Show>>>(_serializerOptions);
    //         }
    //         catch(JsonException ex)
    //         {
    //             _logger.LogError($"-----> Ошибка: {ex.Message}");
    //             return ResponseData<ListModel<Show>>.Error($"Ошибка: {ex.Message}");
    //         }
    //     }
    //     
    //     _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
    //     return ResponseData<ListModel<Show>>.Error(
    //         $"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
    // }

    public async Task<ResponseData<ListModel<Show>>> GetShowListAsync(string? genreNormalizedName, int pageNo = 1)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}shows/genres");

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
        
        if (response.IsSuccessStatusCode)
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

    public async Task<ResponseData<Show>> GetShowByIdAsync(int id)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}shows/{id}");

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<Show>>(_serializerOptions);
            }
            catch(JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<Show>.Error($"Ошибка: {ex.Message}");
            }
        }
        
        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        return ResponseData<Show>.Error(
            $"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
    }

    public Task UpdateShowAsync(int id, Show show, IFormFile? formFile)
    {
        throw new NotImplementedException();
    }

    public Task DeleteShowAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseData<Show>> CreateShowAsync(Show show, IFormFile? formFile)
    {
        var uri = new Uri(_httpClient.BaseAddress.AbsoluteUri + "genres");

        var response = await _httpClient.PostAsJsonAsync(uri, show, _serializerOptions);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<ResponseData<Show>>(_serializerOptions);

            return data;
        }
        
        _logger.LogError($"object was not created. Error: {response.StatusCode.ToString()}");

        return ResponseData<Show>.Error($"Object was not added. Error: {response.StatusCode.ToString()}");
    }
}