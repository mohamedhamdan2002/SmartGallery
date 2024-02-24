using System.Collections.Generic;
using System.Text.Json;
using SmartGallery.Client.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReservationViewModels;
using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Client.Services
{
	public class ReservationsService : IReservationsService
	{
        private readonly HttpClient _httpClient;

        public ReservationsService(HttpClient httpClient)
		{
            _httpClient = httpClient;
        }
        public async Task<ReservationForCreationViewModel?> CreateReservation(int serviceId,string customerId,ReservationForCreationViewModel reservationForCreationViewModel)
        {
            var HttpResponse = await _httpClient.PostAsJsonAsync($"api/reservations?serviceId={serviceId}&customerId={customerId}", reservationForCreationViewModel);
            if(HttpResponse.IsSuccessStatusCode)
            {
                return reservationForCreationViewModel;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteReservation(string customerId, int serviceId)
        {
            await _httpClient.DeleteAsync($"api/reservations/{serviceId}/{customerId}");
        }
        

        public async Task<IEnumerable<ReservationViewModel>?> GetReservationsForCustomerByCustomerId(string id)
        {
            Stream? ReservationsStream = await _httpClient.GetStreamAsync($"api/customer/{id}/reservations");
            if(ReservationsStream is not null)
            {
                IEnumerable<ReservationViewModel> ReservationsForCustomer = await JsonSerializer.DeserializeAsync<IEnumerable<ReservationViewModel>>(ReservationsStream, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }) ?? new List<ReservationViewModel>();
                return ReservationsForCustomer;
            }
            return null;
        }

        public async Task<IEnumerable<ReservationViewModel>?> GetReservationsForServiceByServiceId(int id)
        {
            Stream? ReservationsStream = await _httpClient.GetStreamAsync($"api/service/{id}/reservations");
            if (ReservationsStream is not null)
            {
                IEnumerable<ReservationViewModel> ReservationsForServices = await JsonSerializer.DeserializeAsync<IEnumerable<ReservationViewModel>>(ReservationsStream, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }) ?? new List<ReservationViewModel>();
                return ReservationsForServices;
            }
            return null;
        }

        public async Task<ReservationForUpdateViewModel?> UpdateReservation(int serviceId,string customerId, ReservationForUpdateViewModel reservationForUpdateViewModel)
        {
            var Result = await _httpClient.PutAsJsonAsync($"api/reservations?serviceId={serviceId}&customerId={customerId}", reservationForUpdateViewModel);
            if (Result.IsSuccessStatusCode)
                return reservationForUpdateViewModel;
            else
                return null;
        }
    }
}

