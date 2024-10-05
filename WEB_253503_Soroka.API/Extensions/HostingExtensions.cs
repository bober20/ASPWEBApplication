using WEB_253503_Soroka.API.Services.GenreService;
using WEB_253503_Soroka.API.Services.ShowService;

namespace WEB_253503_Soroka.API.Extensions;

public static class HostingExtensions
{
    public static void RegisterCustomServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IShowService, ShowService>();
    }
}