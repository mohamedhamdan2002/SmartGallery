using System;
using SmartGallery.Client.Services.Contracts;

namespace SmartGallery.Client.Services
{
	public class ReviewsService :IReviewsService
	{

        private readonly HttpClient _httpClient;

        public ReviewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}

