﻿using BlazorApp1.Client.Entities;
using BlazorApp1.Client.Repositories;
using DemoProject.Repositories;
using Microsoft.AspNetCore.Components;

namespace DemoProject.Components.Pages;

public partial class Cursussen : ComponentBase
{
    [Inject] public ICourseRepository CourseRepository { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    
    [SupplyParameterFromForm()] public Course NewCourse { get; set; } = new();

    public string Naam { get; set; } = "JP";

    public static List<Course>? Courses { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Courses = (await CourseRepository.GetAllAsync()).ToList();
    }

    public async Task AddCourse()
    {
        await CourseRepository.Add(NewCourse);
        Courses = (await CourseRepository.GetAllAsync()).ToList();
        NavigationManager.NavigateTo("/CURSUSsen");
    }

    public void DuurtLang()
    {
        Thread.Sleep(3000);
    }
}