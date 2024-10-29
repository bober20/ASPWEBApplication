namespace WEB_253503_Soroka.UI.Services.Authentication;

public interface ITokenAccessor
{
    Task<string> GetAccessTokenAsync();
    Task SetAuthorizationHeaderAsync(HttpClient httpClient);
}