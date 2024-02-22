namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationCustomerDetailsVM(
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime,
        string customerId,
        string customerEmail,
        string Address,
        string PhoneNumber
    );
