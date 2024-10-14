namespace WEB_253503_Soroka.UI.ApiServices.FileServices;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile formFile);
    Task DeleteFileAsync(string fileName);
}