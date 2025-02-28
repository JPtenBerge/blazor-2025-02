using Microsoft.AspNetCore.Identity;

namespace DemoProject.Auth;

public class DemoUser : IdentityUser
{
    public string FavorieteChips { get; set; }
}