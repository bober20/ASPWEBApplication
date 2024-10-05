using Microsoft.AspNetCore.Mvc;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;
using WEB_253503_Soroka.UI.Services.GenreService;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.Controllers;

public class ShowsController: Controller
{
    private IShowService _showService;
    private IGenreService _genreService;
    
    public ShowsController(IShowService showService, IGenreService genreService)
    {
        _showService = showService;
        _genreService = genreService;
    }

    public async Task<IActionResult> Index(string? genre, int pageNo = 1)
    {
        var genreResponse = await _genreService.GetGenreListAsync();
        var showResponse = await _showService.GetShowListAsync(genre, pageNo);

        if (!showResponse.Successfull) return NotFound(showResponse.ErrorMessage);
        if (!genreResponse.Successfull) return NotFound(genreResponse.ErrorMessage);
        
        var request = HttpContext.Request;
        string currentGenreNormalizedName = request.Query["genre"].ToString();

        ViewData["genres"] = genreResponse.Data;

        if (string.IsNullOrEmpty(genre))
        {
            ViewData["currentGenre"] = "Все";
        }
        else
        {
            ViewData["currentGenreNormalizedName"] = currentGenreNormalizedName;
            ViewData["currentGenre"] = genreResponse.Data!.Find(g => g.NormalizedName == genre)!.Name;
        }
        
        return View(showResponse.Data);
    }
}