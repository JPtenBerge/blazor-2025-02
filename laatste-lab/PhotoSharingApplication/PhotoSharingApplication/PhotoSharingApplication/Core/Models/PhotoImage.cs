namespace PhotoSharingApplication.Core.Models;

public class PhotoImage
{
    public int Id { get; set; }
    public byte[]? PhotoFile { get; set; } = [];
    public string? ImageMimeType { get; set; }
}
