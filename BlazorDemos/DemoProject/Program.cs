using BlazorApp1.Client.Repositories;
using DemoProject.Components;
using DemoProject.DataAccess;
using DemoProject.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
// builder.WebHost.

// HTTP/1.1
// HTTP/2 SPDY  alleen maar HTTPS  multiplexing
// HTTP/3 Quality UDP Internet Connections <== 

// TCP => HTTP HTTP HTTP HTTP HTTP HTTP HTTP HTTP <=> TCP
// UDP => HTTP HTTP HTTP HTTP HTTP HTTP HTTP HTTP <=> UDP

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContextFactory<DemoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext"));
});

// builder.Services.AddDbContext<DemoContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext"));
// }, ServiceLifetime.Transient);

// builder.Services.AddTransient<ICourseRepository, CourseRepository>(); // altijd een nieuwe instantie
// builder.Services.AddScoped(); // nieuwe instantie per HTTP-request
// builder.Services.AddSingleton<ICourseRepository, CourseRepository>(); // altijd een nieuwe instantie
builder.Services.AddTransient<ICourseRepository, CourseDbRepository>(); // altijd een nieuwe instantie

// builder.Services.AddSingleton<>(); // 1 instance to rule them all
builder.Services.AddMudServices();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();


app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp1.Client._Imports).Assembly);

app.Run();
