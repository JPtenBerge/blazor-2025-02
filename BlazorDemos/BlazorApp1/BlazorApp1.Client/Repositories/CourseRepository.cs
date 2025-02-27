using System.Net.Http.Json;
using BlazorApp1.Client.Entities;
using Flurl.Http;

namespace BlazorApp1.Client.Repositories;

public class CourseRepository : ICourseRepository
{
    // private readonly HttpClient _http;
    //
    // public CourseRepository(HttpClient http)
    // {
    //     _http = http;
    // }
    
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await "https://localhost:7018/minapi/courses".GetJsonAsync<IEnumerable<Course>>();
    }

    public Task<Course> Add(Course newCourse)
    {
        throw new NotImplementedException();
    }
}