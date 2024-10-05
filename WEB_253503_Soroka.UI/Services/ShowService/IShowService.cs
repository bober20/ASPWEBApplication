using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.Domain.Entities;

namespace WEB_253503_Soroka.UI.Services.ShowService;

public interface IShowService
{
    public Task<ResponseData<ListModel<Show>>> GetShowListAsync(string? genreNormalizedName, int pageNo=1);

    public Task<ResponseData<Show>> GetShowByIdAsync();

    public Task UpdateShowAsync(int id, Show show, IFormFile? formFile);

    public Task DeleteShowAsync(int id);

    public Task CreateShowAsync(Show show, IFormFile? formFile);
}