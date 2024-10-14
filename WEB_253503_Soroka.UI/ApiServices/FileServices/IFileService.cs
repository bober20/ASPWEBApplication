namespace WEB_253503_Soroka.UI.ApiServices;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile formFile);
    Task DeleteFileAsync(string fileName);
}