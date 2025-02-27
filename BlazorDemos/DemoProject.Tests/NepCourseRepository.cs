using BlazorApp1.Client.Entities;
using BlazorApp1.Client.Repositories;

namespace DemoProject.Tests;

public class NepCourseRepository : ICourseRepository
{
    public Task<IEnumerable<Course>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Course> Add(Course newCourse)
    {
        throw new NotImplementedException();
    }
}