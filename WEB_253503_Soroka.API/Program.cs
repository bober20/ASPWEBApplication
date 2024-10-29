using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WEB_253503_Soroka.API.Data;
using WEB_253503_Soroka.API.Extensions;
using WEB_253503_Soroka.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.RegisterCustomServices();

builder.Services.AddAuthorization(opt => { opt.AddPolicy("admin", p => p.RequireRole("POWER-USER")); });

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

var authServer = builder.Configuration
    .GetSection("AuthServer")
    .Get<AuthServerData>();
// Добавить сервис аутентификации
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    JwtBearerDefaults.AuthenticationScheme, o =>
    {
// Адрес метаданных конфигурации OpenID
        o.MetadataAddress = $"{authServer.Host}/realms/{authServer.Realm}/.well-known/openid-configuration";
// Authority сервера аутентификации
        o.Authority = $"{authServer.Host}/realms/{authServer.Realm}";
// Audience для токена JWT
        o.Audience = "account";
// Запретить HTTPS для использования локальной версии Keycloak
// В рабочем проекте должно быть true
        o.RequireHttpsMetadata = false;
    });

var app = builder.Build();

await DbInitializer.SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// app.MapControllerRoute(name: "default", pattern: "api/{controller=Genres}/{action=GetGenre}"); 
app.MapControllers();

app.Run();