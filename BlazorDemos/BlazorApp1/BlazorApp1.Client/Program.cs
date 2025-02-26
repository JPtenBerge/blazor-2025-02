using BlazorApp1.Client.Repositories;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient<ICourseRepository, CourseRepository>();

await builder.Build().RunAsync();