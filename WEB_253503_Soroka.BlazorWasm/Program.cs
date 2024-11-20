using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WEB_253503_Soroka.BlazorWasm;
using WEB_253503_Soroka.BlazorWasm.Services;
using Microsoft.Extensions.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var uriData = new UriData
{
    ApiUri = builder.Configuration.GetSection("UriData").GetValue<string>("ApiUri")!
};

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(uriData.ApiUri) });

builder.Services.AddHttpClient<IShowService, ShowService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Keycloak", options.ProviderOptions);
});

await builder.Build().RunAsync();

