using BlazorApp1.Client.Entities;

namespace DemoProject.Dtos;

public readonly record struct CoursePostRequestDto
{
    public string Title { get; init; }
    public short NrOfDays { get; init; }
    public string FunPhoto { get; init; }
}

public static class CoursePostRequestDtoExtensions
{
    public static Course ToEntity(this CoursePostRequestDto dto)
    {
        return new()
        {
            Title = dto.Title,
            NrOfDays = dto.NrOfDays,
            FunPhoto = dto.FunPhoto
        };
    }

    public static CoursePostRequestDto ToDto(this Course dto)
    {
        return new()
        {
            Title = dto.Title,
            NrOfDays = dto.NrOfDays,
            FunPhoto = dto.FunPhoto
        };
    }
}