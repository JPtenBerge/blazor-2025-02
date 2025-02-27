using Bunit;
using Microsoft.AspNetCore.Components;
using BUnitContext = Bunit.TestContext;

namespace BlazorApp1.Client.Tests;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void TestMethod1()
    {
        var cars = new List<Car> { new() { Make = "Opel", Model = "Astra" } };
        var ctx = new BUnitContext();
        ctx.SetRendererInfo(new RendererInfo("WebAssembly", true));
        var fixture = ctx.RenderComponent<Autocompleter<Car>>(options =>
        {
            options.Add(p => p.Data, cars);
            options.Add(p => p.ItemTemplate, item => $"{item.Make} {item.Model}");
        });
        var sut = fixture.Instance; // system under test
        sut.Query = "e";

        sut.Autocomplete();
        fixture.Render();

        Assert.AreEqual(1, fixture.FindAll("li").Count);
    }
}

class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
}