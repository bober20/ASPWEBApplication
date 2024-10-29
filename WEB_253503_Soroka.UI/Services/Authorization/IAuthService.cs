namespace WEB_253503_Soroka.UI.Services.Authorization;

public interface IAuthService
{
    Task<(bool Result, string ErrorMessage)> RegisterUserAsync(string email,
        string password,
        IFormFile? avatar);
}