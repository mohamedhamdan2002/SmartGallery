namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationForUpdateViewModel : ReservationForManipulationViewModel
{
    public StatusEnum Status { get; init; }
}
