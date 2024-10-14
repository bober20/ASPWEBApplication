using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253503_Soroka.API.Data;
using WEB_253503_Soroka.API.Services.GenreService;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;

namespace WEB_253503_Soroka.API.Services.ShowService;

public class ShowService : IShowService
{
    private List<Show> _shows;
    private readonly List<Genre> _genres;
    private readonly int _maxPageSize = 20;

    private AppDbContext _dbContext;

    public ShowService(IGenreService genreService, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _genres = genreService.GetGenreListAsync().Result.Data;
    }

    public async Task<ResponseData<ListModel<Show>>> GetShowListAsync()
    {
        var query = _dbContext.Shows.AsQueryable();
        var dataList = new ListModel<Show>();
        dataList.Items = await query.ToListAsync();
        return ResponseData<ListModel<Show>>.Success(dataList);
    }
    
    public async Task<ResponseData<ListModel<Show>>> GetShowListAsync(string? genreNormalizedName, int pageNo = 1, int pageSize = 3)
    {
        if (pageSize > _maxPageSize)
        {
            pageSize = _maxPageSize;
        }

        var query = _dbContext.Shows.AsQueryable();
        query = query.Where(show => genreNormalizedName == null || show.Genre!.NormalizedName!.Equals(genreNormalizedName));
        var dataList = new ListModel<Show>();
        var count = await query.CountAsync();

        if (count == 0)
        {
            return ResponseData<ListModel<Show>>.Success(dataList);
        }

        int totalPages = (int)Math.Ceiling((double)count / pageSize);

        if (pageNo > totalPages)
        {
            return ResponseData<ListModel<Show>>.Error("No such page");
        }

        dataList.Items =
            await query.Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(show => show.Id)
                .ToListAsync();
        dataList.CurrentPage = pageNo;
        dataList.TotalPages = totalPages;

        return ResponseData<ListModel<Show>>.Success(dataList);
    }

    public async Task<ResponseData<Show>> GetShowByIdAsync(int id)
    {
        var show = await _dbContext.Shows.FindAsync(id);

        if (show is null)
        {
            return ResponseData<Show>.Error("There is no show with such id.");
        }

        return ResponseData<Show>.Success(show);
    }

    public async Task UpdateShowAsync(int id, Show show)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteShowAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateShowAsync(Show show)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
    {
        throw new NotImplementedException();
    }
}