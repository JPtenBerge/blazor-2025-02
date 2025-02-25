using System.Net.Mime;
using DemoProject.Entities;
using Microsoft.AspNetCore.Components;

namespace DemoProject.Components.Pages;

public partial class FileUpload : ComponentBase
{
    [SupplyParameterFromForm]
    public Park NewPark { get; set; } = new();


    public async Task Process()
    {
        using var stream = NewPark.AlgemeneVoorwaarden.OpenReadStream();
        using var reader = new StreamReader(stream);
        var algVoor = await reader.ReadToEndAsync();
        Console.WriteLine($"algemene voorwaarden: {algVoor}");
        
        // MediaTypeNames.Application.Json
        
    }
}