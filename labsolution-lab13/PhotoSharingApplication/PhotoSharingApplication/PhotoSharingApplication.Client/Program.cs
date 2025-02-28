using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PhotoSharingApplication.Client.Core.Interfaces;
using PhotoSharingApplication.Client.Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();

await builder.Build().RunAsync();
