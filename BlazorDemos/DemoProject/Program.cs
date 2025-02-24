using DemoProject.Components;
using DemoProject.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();


// builder.Services.AddTransient<ICourseRepository, CourseRepository>(); // altijd een nieuwe instantie
// builder.Services.AddScoped(); // nieuwe instantie per HTTP-request
builder.Services.AddSingleton<ICourseRepository, CourseRepository>(); // altijd een nieuwe instantie
// builder.Services.AddSingleton<>(); // 1 instance to rule them all



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
app.MapRazorComponents<App>();

app.Run();
