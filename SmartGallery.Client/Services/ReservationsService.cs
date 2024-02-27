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
            await _httpClient.DeleteAsync($"api/reservations?serviceId={serviceId}&customerId={customerId}");
        }
        

        public async Task<IEnumerable<ReservationServiceDetailsVM>?> GetReservationsForCustomerByCustomerId(string id)
        {
            Stream? ReservationsStream = await _httpClient.GetStreamAsync($"api/customer/{id}/reservations");
            if(ReservationsStream is not null)
            {
                IEnumerable<ReservationServiceDetailsVM> ReservationsForCustomer = await JsonSerializer.DeserializeAsync<IEnumerable<ReservationServiceDetailsVM>>(ReservationsStream, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }) ?? new List<ReservationServiceDetailsVM>();
                return ReservationsForCustomer;
            }
            return null;
        }

        public async Task<IEnumerable<ReservationCustomerDetailsVM>?> GetReservationsForServiceByServiceId(int id)
        {
            Stream? ReservationsStream = await _httpClient.GetStreamAsync($"api/service/{id}/reservations");
            if (ReservationsStream is not null)
            {
                IEnumerable<ReservationCustomerDetailsVM> ReservationsForServices = await JsonSerializer.DeserializeAsync<IEnumerable<ReservationCustomerDetailsVM>>(ReservationsStream, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }) ?? new List<ReservationCustomerDetailsVM>();
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
        public async Task<ReservationForCreationViewModel?> GetReservation(int serviceId, string customerId)
        {
            var Result = await _httpClient.GetStreamAsync($"api/reservations?serviceId={serviceId}&customerId={customerId}");
            if (Result is not null)
            {
                ReservationForCreationViewModel ReservationsForCustomer = await JsonSerializer.DeserializeAsync<ReservationForCreationViewModel>(Result, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }) ?? new ReservationForCreationViewModel();
                return ReservationsForCustomer;
            }
            return null;
        }
        
    }
}

