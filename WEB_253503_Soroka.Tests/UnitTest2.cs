using Microsoft.EntityFrameworkCore;
using WEB_253503_Soroka.API.Data;
using WEB_253503_Soroka.API.Services.ShowService;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;

namespace WEB_253503_Soroka.Tests;

public class ShowsServiceTests
{
    private readonly AppDbContext _dbContext;

    public ShowsServiceTests()
    {
        _dbContext = CreateInMemoryDbContext();
    }
    
    private ShowService CreateShowService()
    {
        var showService = new ShowService(_dbContext);
        return showService;
    }
    
    private AppDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        var genreList = new List<Genre>
        {
            new Genre
            {
                Name = "Ужасы",
                NormalizedName = "horror",
            },
            new Genre
            {
                Name = "Триллер",
                NormalizedName = "thriller",
            },
            new Genre
            {
                Name = "Драма",
                NormalizedName = "drama",
            },
            new Genre
            {
                Name = "Фантастика",
                NormalizedName = "fantasy",
            }
        };

        context.AddRange(genreList);
        
        for (int i = 0; i < 30; i++)
        {
            var show = new Show
            {
                Name = "Show" + i,
                Description = "Description" + i,
                Genre = genreList[i % 4],
                Price = 100 + i
            };
            context.AddRange(show);
            context.SaveChanges();
        }
        
        context.SaveChanges();
        
        return context;
    } 
    
    [Fact]
    public async Task ServiceReturnsFirstPageOfThreeItems()
    {
        var showService = CreateShowService();
        var result = await showService.GetShowListAsync(null);

        Assert.IsType<ResponseData<ListModel<Show>>>(result);
        Assert.True(result.Successfull);
        Assert.Equal(1, result.Data.CurrentPage);
        Assert.Equal(3, result.Data.Items.Count);
        Assert.Equal(10, result.Data.TotalPages);
        Assert.Equal(_dbContext.Shows.First(), result.Data.Items[0]);

        _dbContext.Database.EnsureDeleted();
    }
    
    [Fact]
    public async Task ServiceReturnsCorrectPage()
    {
        var showService = CreateShowService();
        var result = await showService.GetShowListAsync(null, 2);

        Assert.IsType<ResponseData<ListModel<Show>>>(result);
        Assert.True(result.Successfull);
        Assert.Equal(2, result.Data.CurrentPage);
        Assert.Equal(3, result.Data.Items.Count);
        Assert.Equal(10, result.Data.TotalPages);
        
        _dbContext.Database.EnsureDeleted();
    }
    
    [Fact]
    public async Task ServiceReturnsCorrectFilteredData()
    {
        var showService = CreateShowService();
        var result = await showService.GetShowListAsync("thriller");

        Assert.IsType<ResponseData<ListModel<Show>>>(result);
        Assert.True(result.Successfull);
        
        var firstShowOfGenre = _dbContext.Shows.First(show => show.Genre.NormalizedName == "thriller");
        Assert.Equal(firstShowOfGenre, result.Data.Items[0]);
        
        _dbContext.Database.EnsureDeleted();
    }
    
    [Fact]
    public async Task ServiceReturnsCorrectItemsNumber()
    {
        var showService = CreateShowService();
        var result = await showService.GetShowListAsync(null, 1, 40);

        Assert.IsType<ResponseData<ListModel<Show>>>(result);
        Assert.True(result.Successfull);
        
        Assert.Equal(20, result.Data.Items.Count);
        
        _dbContext.Database.EnsureDeleted();
    }
    
    [Fact]
    public async Task ServiceReturnsFalseWithIncorrectNumberOfPages()
    {
        var showService = CreateShowService();
        var result = await showService.GetShowListAsync("thriller", 5, 40);

        Assert.False(result.Successfull);
        
        _dbContext.Database.EnsureDeleted();
    }
}