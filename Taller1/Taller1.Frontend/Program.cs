using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Taller1.Frontend.AuthenticationProviders;
using Taller1.Frontend.Components;
using Taller1.Frontend.Repositories;
using Taller1.Frontend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7289") });
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => x.GetRequiredService<AuthenticationProviderJWT>());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();