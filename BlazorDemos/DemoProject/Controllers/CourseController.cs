using BlazorApp1.Client.Entities;
using BlazorApp1.Client.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _courseRepository;

    public CourseController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Course>> GetAll()
    {
        return await _courseRepository.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Course>> Add(Course newCourse)
    {
        await _courseRepository.Add(newCourse);
        return Created("", newCourse);
    }
}