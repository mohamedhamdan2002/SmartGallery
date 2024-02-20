namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationViewModel(
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime
    );