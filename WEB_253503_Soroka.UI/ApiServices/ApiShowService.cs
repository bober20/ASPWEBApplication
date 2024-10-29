using System.Text;
using System.Text.Json;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.UI.Services.ShowService;
using WEB_253503_Soroka.UI.ApiServices.FileServices;
using WEB_253503_Soroka.UI.Services.Authentication;

namespace WEB_253503_Soroka.UI.ApiServices;

public class ApiShowService : IShowService
{
    private HttpClient _httpClient;
    private ILogger<ApiShowService> _logger;
    private string _pageSize;
    private JsonSerializerOptions _serializerOptions;
    private IFileService _fileService;
    private ITokenAccessor _tokenAccessor;

    public ApiShowService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiShowService> logger,
        IFileService fileService, ITokenAccessor tokenAccessor)
    {
        _httpClient = httpClient;
        _pageSize = configuration.GetSection("ItemsPerPage").Value;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _logger = logger;
        _fileService = fileService;
        _tokenAccessor = tokenAccessor;
    }
    
    public async Task<ResponseData<ListModel<Show>>> GetShowListAsync()
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}shows");

        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Show>>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<ListModel<Show>>.Error($"Ошибка: {ex.Message}");
            }
        }

        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        return ResponseData<ListModel<Show>>.Error(
            $"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
    }

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
        
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Show>>>(_serializerOptions);
            }
            catch (JsonException ex)
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
        
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<Show>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return ResponseData<Show>.Error($"Ошибка: {ex.Message}");
            }
        }

        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        return ResponseData<Show>.Error(
            $"Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
    }

    public async Task UpdateShowAsync(int id, Show show, IFormFile? formFile)
    {
        var uri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}shows/{id}");
        
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        if (formFile is not null)
        {
            await _fileService.DeleteFileAsync(formFile.FileName);
            var newFileName = await _fileService.SaveFileAsync(formFile);
            show.Image = newFileName;
        }
        else
        {
            show.Image = (await GetShowByIdAsync(id)).Data!.Image;
        }

        var response = await _httpClient.PutAsJsonAsync(uri, show);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        }
    }

    public async Task DeleteShowAsync(int id)
    {
        var uri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}shows/{id}");
        
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.DeleteAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode.ToString()}");
        }
    }

    public async Task<ResponseData<Show>> CreateShowAsync(Show show, IFormFile? formFile)
    {
        show.Image = "Images/default-profile-picture.png";

        if (formFile != null)
        {
            var imageUrl = await _fileService.SaveFileAsync(formFile);
            if (!string.IsNullOrEmpty(imageUrl)) 
                show.Image = imageUrl;
        }
        
        var uri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}shows");
        
        await _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);

        var response = await _httpClient.PostAsJsonAsync(uri, show, _serializerOptions);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<ResponseData<Show>>(_serializerOptions);

            return data!;
        }

        _logger.LogError($"object was not created. Error: {response.StatusCode.ToString()}");

        return ResponseData<Show>.Error($"Object was not added. Error: {response.StatusCode.ToString()}");
    }
}