using Microsoft.EntityFrameworkCore;
using WEB_253503_Soroka.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WEB_253503_Soroka.API.Services.GenreService;
using WEB_253503_Soroka.API.Services.ShowService;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = "Data Source=ShowsDataBase.db";
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IShowService, ShowService>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}