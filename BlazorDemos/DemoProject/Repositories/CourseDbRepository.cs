using BlazorApp1.Client.Entities;
using BlazorApp1.Client.Repositories;
using DemoProject.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Repositories;

public class CourseDbRepository : ICourseRepository
{
    private readonly DemoContext _context;
    public CourseDbRepository(DemoContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course> Add(Course newCourse)
    {
        _context.Courses.Add(newCourse);
        await _context.SaveChangesAsync(); // genereert daadwerkelijke SQL en voert dat uit op de db
        return newCourse;
    }
}