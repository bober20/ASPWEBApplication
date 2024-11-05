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
        
        // var showsList = new List<Show>
        // {
        //     new Show
        //     {
        //         Name = "Охотник за разумом",
        //         Description = "Конец 1970-х. Два агента ФБР опрашивают находящихся в заключении серийных убийц с целью понимания их образа мыслей, а также раскрытия текущих преступлений.",
        //         Genre = genreList.Find((genre => genre.NormalizedName.Equals("thriller"))),
        //         Price = 200,
        //     },
        //     new Show
        //     {
        //         Name = "Джентельмены",
        //         Description = "Молодой человек по\u00a0имени Эдди Холстед узнаёт, что полученное им большое наследство связано с наркоимперией Бобби Гласса.",
        //         Genre = genreList.Find((genre => genre.NormalizedName.Equals("comedy"))),
        //         Price = 180,
        //     },
        //     new Show
        //     {
        //         Name = "Табу",
        //         Description = "Дерзкий авантюрист против Англии, США и Ост-Индской компании.",
        //         Genre = genreList.Find((genre => genre.NormalizedName.Equals("drama"))),
        //         Price = 220,
        //     },
        //     new Show
        //     {
        //         Name = "Падение дома Ашеров",
        //         Description = "В семье Ашеров загадочным образом умирают все наследники.",
        //         Genre = genreList.Find((genre => genre.NormalizedName.Equals("horror"))),
        //         Price = 230,
        //     },
        //     new Show
        //     {
        //         Name = "Больница Никербокер",
        //         Description = "Гениальный хирург против системы и наркозависимости.",
        //         Genre = genreList.Find((genre => genre.NormalizedName.Equals("thriller"))),
        //         Price = 230,
        //     },
        //     new Show
        //     {
        //         Name = "История девятихвостого лиса",
        //         Description = "Оборотень находит и реинкарнацию своей первой любви, и древнего врага.",
        //         Genre = genreList.Find((genre => genre.NormalizedName.Equals("fantasy"))),
        //         Price = 230,
        //     },
        //     new Show
        //     {
        //         Name = "Уроки химии",
        //         Description = "В 1950-х годах мечта одной женщины стать ученым сталкивается с общественным мнением, согласно которому место женщин — только в домашней сфере.",
        //         Genre = genreList.Find((genre => genre.NormalizedName.Equals("drama"))),
        //         Price = 230,
        //     }
        // };

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
    }
    
    [Fact]
    public async Task ServiceReturnsCorrectItemsNumber()
    {
        var showService = CreateShowService();
        var result = await showService.GetShowListAsync(null, 1, 40);

        Assert.IsType<ResponseData<ListModel<Show>>>(result);
        Assert.True(result.Successfull);
        
        Assert.Equal(20, result.Data.Items.Count);
    }
    
    [Fact]
    public async Task ServiceReturnsFalseWithIncorrectNumberOfPages()
    {
        var showService = CreateShowService();
        var result = await showService.GetShowListAsync("thriller", 5, 40);

        Assert.False(result.Successfull);
    }
}