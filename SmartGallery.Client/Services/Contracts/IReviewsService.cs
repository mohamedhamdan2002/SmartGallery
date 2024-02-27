using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Client.Services.Contracts;

public interface IReviewsService
{
    Task<IEnumerable<ServiceViewModel>?> GetReviewsAsync();
    Task CreateReview(ServiceForCreationViewModel reviewForCreationViewModel);
    Task DeleteReview(int id);
}

