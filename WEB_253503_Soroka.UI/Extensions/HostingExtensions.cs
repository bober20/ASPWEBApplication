using WEB_253503_Soroka.UI.HelperClasses;
using WEB_253503_Soroka.UI.Services.GenreService;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.Extensions;

public static class HostingExtensions
{
    public static void RegisterCustomServices(this WebApplicationBuilder builder)
    {
        // builder.Services.AddScoped<IGenreService, MemoryGenreService>();
        // builder.Services.AddScoped<IShowService, MemoryShowService>();
        
        builder.Services
            .Configure<KeycloakData>(builder.Configuration.GetSection("Keycloak"));
    }
}