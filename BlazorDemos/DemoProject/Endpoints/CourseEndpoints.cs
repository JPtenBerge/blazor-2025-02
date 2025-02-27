using BlazorApp1.Client.Entities;
using BlazorApp1.Client.Repositories;
using DemoProject.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DemoProject.Endpoints;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder MapCourseEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("minapi/courses").WithTags("Courses");

        group.MapGet("/", GetAll);
        group.MapPost("/", Add);
        group.MapPut("/{id:int}", Edit);

        return endpoints;
    }

    public static async Task<IEnumerable<Course>> GetAll(ICourseRepository courseRepository)
    {
        return await courseRepository.GetAllAsync();
    }

    public static async Task<Created<CoursePostResponseDto>> Add(ICourseRepository courseRepository,
        CoursePostRequestDto dto)
    {
        // validate shizzle
        
        
        var updatedEntity = await courseRepository.Add(dto.ToEntity());
        return TypedResults.Created("", new CoursePostResponseDto { Id = updatedEntity.Id });
    }

    public static async Task<Created<Course>> Edit(ICourseRepository courseRepository, Course newCourse, int id)
    {
        return TypedResults.Created("", await courseRepository.Add(newCourse));
    }
}