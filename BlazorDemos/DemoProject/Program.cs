using System.Security.Claims;
using BlazorApp1.Client.Repositories;
using DemoProject.Auth;
using DemoProject.Components;
using DemoProject.DataAccess;
using DemoProject.Endpoints;
using DemoProject.Repositories;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "cookie";
        // options.DefaultChallengeScheme = "oidc";
        // options.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("cookie", options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = false;
        options.Cookie.Name = "__Host-spa";
        options.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name" 
            //, RoleClaimType = "role"
        };
        
        options.Authority = "https://localhost:5010";

        // confidential client using code flow + PKCE
        options.ClientId = "interactive";
        options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
        options.ResponseType = "code";

        // query response type is compatible with strict SameSite mode
        options.ResponseMode = "query";

        // get claims without mappings
        options.MapInboundClaims = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        // save tokens into authentication session
        // to enable automatic token management
        options.SaveTokens = true;

        // request scopes
        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("coursesapi");

        // and refresh token
        // options.Scope.Add("offline_access");
    });


builder.Services.AddControllers();

// builder.Services.AddIdentity<DemoUser, IdentityRole>().AddEntityFrameworkStores<DemoContext>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContextFactory<DemoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext"));
});

builder.Services.AddTransient<HttpClient>();

// builder.Services.AddDbContext<DemoContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext"));
// }, ServiceLifetime.Transient);

builder.Services.AddTransient<ICourseRepository, CourseDbRepository>();
builder.Services.AddMudServices();
builder.Services.AddOpenApi();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseAuthentication();
app.UseAuthorization();

app
    .UseHttpsRedirection()
    .UseAntiforgery();

app.MapStaticAssets();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.MapControllers(); // project doorzoeken naar controllers

app.MapCourseEndpoints();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorApp1.Client._Imports).Assembly);

app.Run();