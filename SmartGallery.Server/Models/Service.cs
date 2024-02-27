using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Server.Models;
public class Service : BaseEntity
{
    public string Icon { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Item> Items { get; set; } = new List<Item>();
    public ServiceViewModel ToViewModel()
    {
        return new ServiceViewModel (
            Icon: this.Icon,
            Id: this.Id,
            Name: this.Name,
            Description: this.Description
        );
    }
}