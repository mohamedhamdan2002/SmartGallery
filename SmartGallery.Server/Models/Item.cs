namespace SmartGallery.Server.Models;
public class Item : BaseEntity
{
    public string Name { get; set;  } = null!;
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
