using WEB_253503_Soroka.API.Data;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;

namespace WEB_253503_Soroka.API.Services.GenreService;

public class GenreService : IGenreService
{
    private AppDbContext _dbContext;

    public GenreService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ResponseData<List<Genre>>> GetGenreListAsync()
    {
        var query = _dbContext.Genres.AsQueryable();
        var dataList = new List<Genre>(query);

        return ResponseData<List<Genre>>.Success(dataList);
    }
}