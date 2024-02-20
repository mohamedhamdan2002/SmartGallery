namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationForCreationViewModel : ReservationForManipulationViewModel
{
    public string? ProblemDescription { get; init; }
}

