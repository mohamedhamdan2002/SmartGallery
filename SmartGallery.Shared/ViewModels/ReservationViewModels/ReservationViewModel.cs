using SmartGallery.Shared.ViewModels.ItemViewModels;

namespace SmartGallery.Shared.ViewModels.ReservationViewModels;

public record ReservationViewModel(
        int Id,
        string ProblemDescription,
        StatusEnum Status,
        DateOnly ReservationDate,
        TimeOnly ReservationTime,
        int ItemId
    );
public record ReservationDetailsVM
{
    public ReservationDetailsVM(int id, string problemDescription, StatusEnum status, DateOnly reservationDate, TimeOnly reservationTime, string customerId, string customerEmail, int serviceId, string serviceName, int itemId, string itemName)
    {
        Id = id;
        ProblemDescription = problemDescription;
        Status = status;
        ReservationDate = reservationDate;
        ReservationTime = reservationTime;
        this.customerId = customerId;
        this.customerEmail = customerEmail;
        this.serviceId = serviceId;
        this.serviceName = serviceName;
        ItemId = itemId;
        this.itemName = itemName;
    }

    public int Id {get ;set;}
    public string ProblemDescription {get ;set;}
    public StatusEnum Status {get ;set;}
    public DateOnly ReservationDate {get ;set;}
    public TimeOnly ReservationTime {get ;set;}
    public string customerId {get ;set;}
    public string customerEmail {get ;set;}
    public int serviceId {get ;set;}
    public string serviceName {get ;set;}
    public int ItemId {get ;set;}
    public string itemName {get ;set;}
}
