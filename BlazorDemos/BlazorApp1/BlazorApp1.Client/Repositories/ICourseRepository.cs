using BlazorApp1.Client.Entities;

namespace BlazorApp1.Client.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> Add(Course newCourse);
}