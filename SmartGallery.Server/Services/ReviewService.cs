using SmartGallery.Server.Exceptions;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Server.Utilities;
using SmartGallery.Shared.ViewModels.ReviewViewModels;

namespace SmartGallery.Server.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserService _userService;

        public ReviewService(IRepositoryManager repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }
        public async Task<ReviewViewModel> CreateReviewForService(int reservationId, string customerId, ReviewForCreationVM model)
        {
            await CheckIfCustomerExistsAsync(customerId);
            await CheckIfReservationExistsAsync(reservationId);
            var review = new Review
            {
                ReservationId = reservationId,
                CustomerId = customerId,
                Rating = model.Rating,
                Comment = model.Comment,
            };
            await _repository.Review.CreateAsync(review);
            await _repository.SaveChangesAsync();
            var reviewViewModel = new ReviewViewModel(review.Id, customerId, reservationId, review.Rating, review.Comment);
            return reviewViewModel;
        }
        private async Task CheckIfReservationExistsAsync(int reservationId)
        {
            var isExist = await _repository.Reservation.CheckIfReservationExistAsync(reservationId);
            if (!isExist)
                throw new NotFoundException($"the service with id: {reservationId} doesn't exist in the database.");
        }
        private async Task CheckIfCustomerExistsAsync(string customerId)
        {
            var isExist = await _userService.CheckIfUserExistByIdAsync(customerId);
            if (!isExist)
                throw new NotFoundException($"the customer with id: {customerId} doesn't exist in the database.");
        }

        public async Task<IEnumerable<ReviewDetailsVM>> GetReviewsAsync()
        {

            var reviews = await _repository.Review.GetReviewsAsync(
                    selector: x => new ReviewDetailsVM(
                        x.Id,
                        x.Customer.Email!,
                        x.Rating,
                        x.Comment!
                    ),
                    trackChanges: false,
                    NavigationProperties.Customer
                );
            return reviews;   
        }
    }
}
