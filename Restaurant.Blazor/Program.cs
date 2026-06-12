using Restaurant.Blazor.Components;
using Restaurant.Blazor.Services.Implementations;
using Restaurant.Blazor.Services.Implementations.Catalog;
using Restaurant.Blazor.Services.Interfaces;
using Restaurant.Blazor.Services.Interfaces.Catalog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Service Scoped
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IProductApiService, ProductApiService>();

// Configure HttpClient
builder.Services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["WebAPI:BaseUrl"] ?? "https://localhost:7100");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
