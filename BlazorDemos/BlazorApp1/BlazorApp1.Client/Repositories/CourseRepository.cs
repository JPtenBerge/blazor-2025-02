using BlazorApp1.Client.Entities;

namespace BlazorApp1.Client.Repositories;

public class CourseRepository : ICourseRepository
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