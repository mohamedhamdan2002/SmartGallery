using System;
using System.Net.Http;
using System.Text.Json;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReviewViewModels;
using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Client.Services
{
	public class ReviewsService :IReviewsService
	{

        private readonly HttpClient _httpClient;

        public ReviewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateReview(ReviewForCreationVM reviewForCreationViewModel)
        {
            await _httpClient.PostAsJsonAsync("api/Reviews", reviewForCreationViewModel);
        }

        public async Task DeleteReview(int id)
        {
            await _httpClient.DeleteAsync($"api/Reviews/{id}");
        }

        public async Task<IEnumerable<ReviewDetailsVM>?> GetReviewsAsync()
        {
            var Services = await JsonSerializer.DeserializeAsync<IEnumerable<ReviewDetailsVM>>
                           (await _httpClient.GetStreamAsync("api/Reviews"), new JsonSerializerOptions()
                           { PropertyNameCaseInsensitive = true }) ?? new List<ReviewDetailsVM>();
            return Services;
        }
    }
}