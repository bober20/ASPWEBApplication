using WEB_253503_Soroka.API.Data;
using WEB_253503_Soroka.UI.Services.GenreService;
using WEB_253503_Soroka.UI.Services.ShowService;
using WEB_253503_Soroka.UI.ApiServices;
using WEB_253503_Soroka.UI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// builder.RegisterCustomServices();

UriData uriData = builder.Configuration.GetRequiredSection("UriData").Get<UriData>()!;
builder.Services.AddHttpClient<IGenreService, ApiGenreService>(opt => opt.BaseAddress=new Uri(uriData.ApiUri));
builder.Services.AddHttpClient<IShowService, ApiShowService>(opt => opt.BaseAddress=new Uri(uriData.ApiUri));

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "AreaAdmin",
    areaName: "Admin",
    pattern: "Admin/{controller=Admin}/{action=Index}");

app.Run();
