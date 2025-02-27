using BlazorApp1.Client.Repositories;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient<ICourseRepository, CourseRepository>();
// builder.Services.AddTransient<HttpClient>();

await builder.Build().RunAsync();