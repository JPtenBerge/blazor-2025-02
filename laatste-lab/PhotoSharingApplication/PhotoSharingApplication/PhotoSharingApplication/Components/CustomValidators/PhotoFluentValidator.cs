using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using FluentValidation;
using PhotoSharingApplication.Core.Models;


namespace PhotoSharingApplication.Components.CustomValidators;

public class PhotoFluentValidator : ComponentBase {
    private ValidationMessageStore? messageStore;

    [CascadingParameter]
    private EditContext? CurrentEditContext { get; set; }

    [Inject]
    public IValidator<Photo> Validator { get; set; }

    protected override void OnInitialized()
    {
        if (CurrentEditContext is null)
        {
            throw new InvalidOperationException(
                $"{nameof(PhotoFluentValidator)} requires a cascading " +
                $"parameter of type {nameof(EditContext)}. " +
                $"For example, you can use {nameof(PhotoFluentValidator)} " +
                $"inside an {nameof(EditForm)}.");
        }

        messageStore = new(CurrentEditContext);

        CurrentEditContext.OnValidationRequested += (s, e) => DisplayErrors();
        CurrentEditContext.OnFieldChanged += (s, e) => DisplayErrors();
    }

    public void DisplayErrors()
    {
        if (CurrentEditContext is not null)
        {
            messageStore?.Clear();
            Dictionary<string, List<string>> errors = Validator.Validate(CurrentEditContext.Model as Photo).Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToList()
                );
            foreach (var err in errors)
            {
                messageStore?.Add(CurrentEditContext.Field(err.Key), err.Value);
            }

            CurrentEditContext.NotifyValidationStateChanged();
        }
    }
}