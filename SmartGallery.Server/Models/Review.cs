namespace SmartGallery.Server.Models;
public class Review
{
    public string CustomerId { get; set; } = null!;
    public Customer Customer { get; set; } = null!;

    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;

    public int Stars { get; set; }
    public string? Comment { get; set; }
}
