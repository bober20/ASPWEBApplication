using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.Domain.Entities;

namespace WEB_253503_Soroka.API.Services.ShowService;

public interface IShowService
{
    public Task<ResponseData<ListModel<Show>>> GetShowListAsync();

    public Task<ResponseData<ListModel<Show>>> GetShowListAsync(string? genreNormalizedName, int pageNo=1, int pageSize = 3);

    public Task<ResponseData<Show>> GetShowByIdAsync(int id);

    public Task UpdateShowAsync(int id, Show show);

    public Task DeleteShowAsync(int id);

    public Task<ResponseData<Show>> CreateShowAsync(Show show);

    public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
}