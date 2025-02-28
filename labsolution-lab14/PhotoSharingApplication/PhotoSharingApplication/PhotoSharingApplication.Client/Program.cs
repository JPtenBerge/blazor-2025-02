using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhotoSharingApplication.Client;
using PhotoSharingApplication.Client.Core.Interfaces;
using PhotoSharingApplication.Client.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();


await builder.Build().RunAsync();
