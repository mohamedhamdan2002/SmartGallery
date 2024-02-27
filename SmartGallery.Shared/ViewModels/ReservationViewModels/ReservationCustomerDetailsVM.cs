namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationCustomerDetailsVM(
        int Id,
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime,
        string customerId,
        string customerEmail,
        string Address,
        string PhoneNumber,
        int ItemId,
        string ItemName
    );
