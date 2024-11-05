using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using WEB_253503_Soroka.API.Services.GenreService;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.UI.ApiServices;
using WEB_253503_Soroka.UI.ApiServices.FileServices;
using WEB_253503_Soroka.UI.Controllers;
using WEB_253503_Soroka.UI.Services.Authentication;
using WEB_253503_Soroka.UI.Services.GenreService;
using WEB_253503_Soroka.UI.Services.ShowService;
using Xunit.Abstractions;
using IGenreService = WEB_253503_Soroka.UI.Services.GenreService.IGenreService;

namespace WEB_253503_Soroka.Tests;

public class ShowsControllerTests
{
    ServiceCollection services = new ServiceCollection();
    private readonly IConfiguration _configuration = Substitute.For<IConfiguration>();

    private readonly IGenreService _genreService;
    private readonly IShowService _showService;
    
    private readonly ITestOutputHelper _output;
    
    private ShowsController CreateController()
    {
        return new ShowsController(_showService, _genreService);
    }

    public ShowsControllerTests(ITestOutputHelper output)
    {
        
        // var inMemorySettings = new Dictionary<string, string>()
        // {
        //     { "ItemsPerPage", "3" },
        //     { "UriData:ApiUri", "https://localhost:7002/api/" },
        //     { "Keycloak:Host", "http://localhost:8080" },
        //     { "Keycloak:Realm", "Soroka" },
        //     { "Keycloak:ClientId", "SorokaUIClient" },
        //     { "Keycloak:ClientSecret", "FLDmqReq9kxBzFoIr2cfpuJyXLG3GJjl" }
        // };
        //
        // IConfiguration configuration = new ConfigurationBuilder()
        //     .AddInMemoryCollection(inMemorySettings)
        //     .Build();
        
        services.AddSingleton<IConfiguration>(_configuration);
        services.AddScoped<IGenreService, MemoryGenreService>();
        services.AddScoped<IShowService, MemoryShowService>();
        _output = output;
        
        var serviceProvider = services.BuildServiceProvider();
        
        _genreService = serviceProvider.GetRequiredService<IGenreService>();
        _showService = serviceProvider.GetRequiredService<IShowService>();
    }

    [Fact]
    public async Task IndexReturnCorrectGenre()
    {
        var controller = CreateController();
        
        controller.ControllerContext.HttpContext = new DefaultHttpContext();
        
        var result = await controller.Index("thriller");
        var okResult = Assert.IsType<ViewResult>(result);
        
        Assert.NotNull(okResult.ViewData["currentGenre"]);
        Assert.Equal("Триллер", (okResult.ViewData["currentGenre"]));
    }

    [Fact]
    public async Task IndexReturnsAllGenres()
    {
        var controller = CreateController();
        
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = await controller.Index("");
        var okResult = Assert.IsType<ViewResult>(result);
        
        Assert.NotNull(okResult.ViewData["currentGenre"]);
        Assert.Equal("Все", okResult.ViewData["currentGenre"]);
    }
    
    [Fact]
    public async Task IndexReturnsCorrectModel()
    {
        var controller = CreateController();
        
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = await controller.Index(null);

        var okResult = Assert.IsType<ViewResult>(result);

        var model = okResult.Model as ListModel<Show>;
        Assert.NotNull(model);
        Assert.Equal(1, model.CurrentPage);
        Assert.Equal(4, model.TotalPages);
        Assert.Equal(3, model.Items.Count);

    }
    
    [Fact]
    public async Task IndexReturnsNotFoundWhenGenreDoesNotExist()
    {
        var controller = CreateController();
        
        controller.ControllerContext.HttpContext = new DefaultHttpContext();

        var result = await controller.Index("nonexistantgenre");

        Assert.IsType<NotFoundObjectResult>(result);
    }
}