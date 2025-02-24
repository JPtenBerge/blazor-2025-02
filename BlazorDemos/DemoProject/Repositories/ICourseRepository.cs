using DemoProject.Entities;

namespace DemoProject.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> Add(Course newCourse);
}