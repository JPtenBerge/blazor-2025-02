using PhotoSharingApplication.Client.Core.Models;

namespace PhotoSharingApplication.Core.Models;

public class Photo
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public string? DataUrl => PhotoImage?.PhotoFile == null ? null : $"data:{PhotoImage.ImageMimeType};base64,{Convert.ToBase64String(PhotoImage.PhotoFile)}";
    public string Owner { get; set; }
    public PhotoImage? PhotoImage { get; set; } = new PhotoImage();

    public List<Comment> Comments { get; set; } = [];
}
