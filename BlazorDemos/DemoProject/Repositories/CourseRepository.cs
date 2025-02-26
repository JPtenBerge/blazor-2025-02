using BlazorApp1.Client.Entities;
using BlazorApp1.Client.Repositories;

namespace DemoProject.Repositories;

public class CourseRepository : ICourseRepository
{
    private List<Course> s_courses =
    [
        new()
        {
            Id = 4,
            Title = "Awesome Blazorrrr",
            NrOfDays = 5,
            FunPhoto = "https://www.delta-n.nl/wp-content/uploads/2019/10/BrandBlazor_300.png"
        },
        new()
        {
            Id = 8,
            Title = "Awesome C#",
            NrOfDays = 5,
            FunPhoto = "https://miro.medium.com/v2/resize:fit:375/1*NhpIIUL7AFgKKn30gKoDUw.png"
        },
        new()
        {
            Id = 15,
            Title = "Matige React",
            NrOfDays = 2,
            FunPhoto = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/React.svg/1200px-React.svg.png"
        }
    ];

    public Task<IEnumerable<Course>> GetAllAsync()
    {
        return Task.FromResult(s_courses.AsEnumerable());
    }

    public Task<Course> Add(Course newCourse)
    {
        newCourse.Id = s_courses.Max(x => x.Id) + 1;
        s_courses.Add(newCourse);
        return Task.FromResult(newCourse);
    }
}