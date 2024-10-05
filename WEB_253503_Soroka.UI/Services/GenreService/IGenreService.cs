using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.Domain.Entities;

namespace WEB_253503_Soroka.UI.Services.GenreService;

public interface IGenreService
{
    public Task<ResponseData<List<Genre>>> GetGenreListAsync();
}