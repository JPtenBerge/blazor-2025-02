using BlazorApp1.Client.Entities;
using BlazorApp1.Client.Repositories;
using DemoProject.Endpoints;
using Moq;

namespace DemoProject.Tests;

[TestClass]
public sealed class CourseEndpointsTests
{
    private List<Course> _courses = null!;
    private Mock<ICourseRepository> _courseRepositoryMoq = null!;
    
    [TestInitialize]
    public void Init()
    {
        _courses = new List<Course> { new(), new(), new() };
        _courseRepositoryMoq = new Mock<ICourseRepository>();
        _courseRepositoryMoq.Setup(x => x.GetAllAsync()).ReturnsAsync(_courses);
    }
    
    // functie+watjetest+verwachteuitkomst
    // Add_To_Shopping_Cart_Should_Increase_Price()
    
    [TestMethod]
    public async Task GetAll_Nothing_RepositoryCalledAndDataReturned()
    {
        var response = await CourseEndpoints.GetAll(_courseRepositoryMoq.Object);

        _courseRepositoryMoq.Verify(x => x.GetAllAsync(), Times.Once);
        Assert.AreEqual(3, response.Count());
    }
}