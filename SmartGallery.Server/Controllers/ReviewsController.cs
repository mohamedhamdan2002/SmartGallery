using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Server.Models;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReviewViewModels;

namespace SmartGallery.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
            => _service = service;
        [HttpGet]
        public async Task<IActionResult> GetReviews()
            => Ok(await _service.GetReviewsAsync());
        [HttpPost]
        public async Task<IActionResult> CreateReviewForReservation([FromQuery] int reservationId, string customerId, [FromBody] ReviewForCreationVM model)
        {
            if (reservationId is default(int) || string.IsNullOrEmpty(customerId))
                return BadRequest("Invalid parameters");
            
            if (model is null)
                return BadRequest("ReviewForCreationVM object is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var reviewViewModel = await _service.CreateReviewForService(reservationId, customerId, model);
            return Ok(reviewViewModel);
        }
        
    }
}
