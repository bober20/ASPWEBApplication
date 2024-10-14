using System.Text;

namespace WEB_253503_Soroka.UI.ApiServices.FileServices;

public class ApiFileService : IFileService
{
    private readonly HttpClient _httpClient;

    public ApiFileService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> SaveFileAsync(IFormFile formFile)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post
        };

        var extension = Path.GetExtension(formFile.FileName);
        var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);

        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(formFile.OpenReadStream());
        content.Add(streamContent, "file", newName);
        
        request.Content = content;
        
        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return String.Empty;
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var response = await _httpClient.DeleteAsync(fileName);
    }
}


// using Microsoft.AspNetCore.Http;
// using System.Net.Http.Headers;
// using System.Text;
//
// namespace WEB_253504_LIANHA.Services
// {
//     public class ApiFileService : IFileService
//     {
//         private readonly HttpClient _httpClient;
//         public ApiFileService(HttpClient httpClient)
//         {
//             _httpClient = httpClient;
//             //_httpContext = httpContextAccessor.HttpContext;
//         }
//         public async Task DeleteFileAsync(string fileUri)
//         {
//             var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}imageupload/{fileUri}").ToString();
//             var response = await _httpClient.DeleteAsync(urlString);
//         }
//         public async Task<string> SaveFileAsync(IFormFile formFile)
//         {
//             var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}imageupload").ToString();
//
//             var extension = Path.GetExtension(formFile.FileName);
//             var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);
//
//             var content = new MultipartFormDataContent();
//
//             var streamContent = new StreamContent(formFile.OpenReadStream());
//             streamContent.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);
//
//             content.Add(streamContent, "file", newName);
//
//             var response = await _httpClient.PostAsync(urlString, content);
//             if (response.IsSuccessStatusCode)
//             {
//                 // Вернуть полученный Url сохраненного файла
//                 return await response.Content.ReadAsStringAsync();
//             }
//             return String.Empty;
//         }
//
//     }
// }