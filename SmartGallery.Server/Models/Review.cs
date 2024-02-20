namespace SmartGallery.Server.Models;
public class Review
{
    public string CustomerId { get; set; } = null!;
    public int ServiceId { get; set; }
    public Customer Customer { get; set; } = null!;
    public Service Service { get; set; } = null!;
    public string? Comment { get; set; }
}
