using SmartGallery.Server.Exceptions;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services.Contracts;
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
        public async Task<ReviewViewModel> CreateReviewForService(int serviceId, string customerId, ReviewForCreationVM model)
        {
            await CheckIfCustomerExistsAsync(customerId);
            await CheckIfServiceExistsAsync(serviceId);
            var review = new Review
            {
                ServiceId = serviceId,
                CustomerId = customerId,
                Rating = model.Rating,
                Comment = model.Comment,
            };
            await _repository.Review.CreateAsync(review);
            await _repository.SaveChangesAsync();
            var reviewViewModel = new ReviewViewModel(review.Id, customerId, serviceId, review.Rating, review.Comment);
            return reviewViewModel;
        }
        private async Task CheckIfServiceExistsAsync(int serviceId)
        {
            var isExist = await _repository.Service.CheckIfServiceExistAsync(serviceId);
            if (!isExist)
                throw new NotFoundException($"the service with id: {serviceId} doesn't exist in the database.");
        }
        private async Task CheckIfCustomerExistsAsync(string customerId)
        {
            var isExist = await _userService.CheckIfUserExistByIdAsync(customerId);
            if (!isExist)
                throw new NotFoundException($"the customer with id: {customerId} doesn't exist in the database.");
        }

        //public async Task<IEnumerable<ReviewDetailsVM>> GetReviewsForService(int serviceId)
        //{
        //    await CheckIfServiceExistsAsync(serviceId);
        //    var reviews = _repository.Review.FindReviewsAsync(
        //            predicate: x => x.ServiceId == serviceId,
        //            selector: x => 
        //        )
        //}
    }
}
