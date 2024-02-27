using SmartGallery.Shared;
using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Server.Models;

public class Reservation : BaseEntity
{
    public string CustomerId { get; set; } = null!;
    public int ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public string ProblemDescription { get; set; } = null!;
    public StatusEnum Status { get; set; } = StatusEnum.Pending;
    public TimeOnly ReservationTime { get; set; }
    public DateOnly ReservationDate { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;
    
    public ReservationViewModel ToViewModel()
    {
        return new ReservationViewModel(
            Id: this.Id,
            ProblemDescription: this.ProblemDescription,
            Status: this.Status,
            ReservationDate: this.ReservationDate,
            ReservationTime: this.ReservationTime,
            ItemId: this.ItemId
        );
    }
}
