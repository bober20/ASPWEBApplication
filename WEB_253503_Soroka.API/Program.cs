public class Program
{
    public static void Main(string[] args)
        => CreateHostBuilder(args).Build().Run();

    // EF Core uses this method at design time to access the DbContext
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}


// using Microsoft.EntityFrameworkCore;
// using WEB_253503_Soroka.API.Data;
// using WEB_253503_Soroka.API.Extensions;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.AddControllers();
//
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// builder.RegisterCustomServices();
//
// var connectionString = builder.Configuration.GetConnectionString("Default");
// builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
//
// var app = builder.Build();
//
// await DbInitializer.SeedData(app);
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
// app.UseStaticFiles();
//
// // app.MapControllerRoute(name: "default", pattern: "api/{controller=Genres}/{action=GetGenre}"); 
// app.MapControllers();
//
// app.Run();
