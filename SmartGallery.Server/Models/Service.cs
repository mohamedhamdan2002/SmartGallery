namespace SmartGallery.Server.Models;
public class Service : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}