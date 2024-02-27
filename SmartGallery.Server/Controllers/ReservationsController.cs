using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Server.Models;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared.ViewModels.ReservationViewModels;

namespace SmartGallery.Server.Controllers;

[Route("api/")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationsController(IReservationService service)
        => _service = service;

    [HttpGet("reservations")]
    public async Task<IActionResult> GetReservations([FromQuery] int serviceId, string? customerId)
    {
        if (serviceId is not default(int) && customerId is not null)
            return Ok(await _service.GetReservationAsync(serviceId, customerId));
        return Ok(await _service.GetReservationsAsync());
    }
    [HttpGet("reservations/{id:int}")]
    public async Task<IActionResult> GetReservationById(int id)
    => Ok(await _service.GetReservationByIdAsync(id));

    [HttpGet("service/{serviceId}/reservations")]
    public async Task<IActionResult> GetReservationsForService(int serviceId)
        => Ok(await _service.GetReservationsForServiceAsync(serviceId));

    [HttpGet("customer/{customerId}/reservations")]
    public async Task<IActionResult> GetReservationsForCustomer(string customerId)
        => Ok(await _service.GetReservationsForCustomerAsync(customerId));

    [HttpPost("[controller]")]
    public async Task<IActionResult> CreateReservation([FromQuery] int serviceId, string customerId, [FromBody] ReservationForCreationViewModel model)
    {
        if (model is null)
            return BadRequest("ReservationForCreationViewModel object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var reservationViewModel = await _service.CreateReservationAsync(serviceId, customerId, model);
        return Ok(reservationViewModel);
    }
    [HttpPut("[controller]/{id:int}")]
    public async Task<IActionResult> UpdateReservation(int id, [FromBody] ReservationForUpdateViewModel model)
    {
        if (model is null)
            return BadRequest("ReservationForUpdateViewModel object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.UpdateReservationAsync(id, model);
        return NoContent();
    }
    [HttpDelete("reservations/{id:int}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        if(id is not default(int) )
        {
           await _service.DeleteReservationAsync(id);
           return NoContent();
        }
        return BadRequest("Delete isn't Completed Successfully");
    }
}
