global using System.Text.Json;
global using Microsoft.Extensions.Logging;
global using PersonalCloud.Portal.Shared;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PersonalCloud.Portal.Client;
using PersonalCloud.Portal.Client.Features.WeatherData;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Used services:
builder.Services.AddScoped<WeatherDataService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
