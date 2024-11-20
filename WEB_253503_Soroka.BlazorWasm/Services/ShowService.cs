using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace WEB_253503_Soroka.BlazorWasm.Services;

public class ShowService: IShowService
{
    public event Action? DataLoaded;
    public List<Genre> Genres { get; set; }
    public List<Show> Shows { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; } = 1;
    public Genre SelectedGenre { get; set; } = null;
    
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IAccessTokenProvider _tokenProvider;
    private ILogger<ShowService> _logger;
    
    
    public ShowService(HttpClient httpClient, IConfiguration configuration, IAccessTokenProvider tokenProvider, 
	    ILogger<ShowService> logger)
	{
	    _httpClient = httpClient;
	    _configuration = configuration;
	    _tokenProvider = tokenProvider;
	    _logger = logger;
	}
    
    public async Task GetShowListAsync(int pageNo = 1)
    {
	    var tokenRequest = await _tokenProvider.RequestAccessToken();
	    if (!tokenRequest.TryGetToken(out var token))
	    {
		    return;
	    }
	    
	    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Value);
	    var pageSize = _configuration.GetSection("ItemsPerPage").Value;
	    
	    var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}shows/genres");
	    List<KeyValuePair<string, string>> queryData = new();

	    if (SelectedGenre is not null)
	    {
		    urlString.Append($"/{SelectedGenre.NormalizedName}");
	    }
	    
	    if (pageNo > 1)
	    {
		    queryData.Add(KeyValuePair.Create("pageNo", pageNo.ToString()));
	    }

	    if (!pageSize.Equals("3"))
	    {
		    queryData.Add(KeyValuePair.Create("pageSize", pageSize));
	    }
	    
	    if (queryData.Count > 0)
	    {
		    urlString.Append(QueryString.Create(queryData));
	    }
			
	    var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

	    if (response.IsSuccessStatusCode)
	    {
		    var data = await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Show>>>();
		    Shows = data.Data.Items;
		    Success = data.Successfull;
		    TotalPages = data.Data.TotalPages;
		    CurrentPage = data.Data.CurrentPage;
		    ErrorMessage = data.ErrorMessage ?? "";
		    DataLoaded?.Invoke();
	    }
    }

    public async Task GetGenreListAsync()
    {
	    var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}genres");
        
	    var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
        
	    if (response.IsSuccessStatusCode)
	    {
		    try
		    {
			    var data = await response.Content.ReadFromJsonAsync<ResponseData<List<Genre>>>();
			    Genres = data!.Data!;
			    ErrorMessage = data.ErrorMessage ?? "";
		    }
		    catch(JsonException ex)
		    {
			    ErrorMessage = ex.Message;
		    }
	    }
    }
}
