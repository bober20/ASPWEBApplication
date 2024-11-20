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
        var controller = new ShowsController(_showService, _genreService);
        controller.ControllerContext.HttpContext = new DefaultHttpContext();
        
        return controller;
    }

    public ShowsControllerTests(ITestOutputHelper output)
    {
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
        Assert.Contains("Триллер", okResult.ViewData["currentGenre"].ToString());
    }

    [Fact]
    public async Task IndexReturnsAllGenres()
    {
        var controller = CreateController();

        var result = await controller.Index("");
        var okResult = Assert.IsType<ViewResult>(result);
        
        Assert.NotNull(okResult.ViewData["currentGenre"]);
        Assert.Contains("Все", okResult.ViewData["currentGenre"].ToString());
    }
    
    [Fact]
    public async Task IndexReturnsCorrectModel()
    {
        var controller = CreateController();

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

        var result = await controller.Index("nonexistantgenre");

        Assert.IsType<NotFoundObjectResult>(result);
    }
    
    [Fact]
    public async Task IndexReturnsNotFoundWhenShowsDontNotExist()
    {
        var controller = CreateController();

        var result = await controller.Index("adventure");
        var okResult = Assert.IsType<ViewResult>(result);
        
        var model = okResult.Model as ListModel<Show>;
        
        Assert.Equal(0, model.Items.Count);
    }
}