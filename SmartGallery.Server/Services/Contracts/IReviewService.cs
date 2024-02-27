using SmartGallery.Shared.ViewModels.ReviewViewModels;

namespace SmartGallery.Server.Services.Contracts
{
    public interface IReviewService
    {
        public Task<ReviewViewModel> CreateReviewForService(int reservationId, string customerId, ReviewForCreationVM model);
        public Task<IEnumerable<ReviewDetailsVM>> GetReviewsAsync();
    }
}
