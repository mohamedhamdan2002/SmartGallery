namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationServiceDetailsVM(
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime,
        int serviceId,
        string serviceName
    );