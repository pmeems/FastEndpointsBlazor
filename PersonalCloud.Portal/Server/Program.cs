global using PersonalCloud.Portal.Shared;
global using Microsoft.Extensions.Logging;
global using FastEndpoints;
global using FastEndpoints.Validation;

using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

// https://www.youtube.com/watch?v=z32_7KgCr6c:
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// https://www.youtube.com/watch?v=A_Bs4g235LU
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseDefaultFiles();
//app.UseRouting();

// https://dev.to/djnitehawk/building-rest-apis-in-net-6-the-easy-way-3h0d:
app.UseAuthorization();
app.UseFastEndpoints(c =>
{
    c.RoutingOptions = o => o.Prefix = "api";  // https://fast-endpoints.com/wiki/Configuration-Settings.html#global-route-prefix
});

// app.MapGet("test", () => "ok!!!!!!!!!!!!!!!");

app.UseOpenApi();
app.UseSwaggerUi3(c => c.ConfigureDefaults());

app.MapRazorPages();
app.MapControllers();
//app.MapFallbackToFile("index.html");

app.Run();
