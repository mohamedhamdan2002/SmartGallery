using Microsoft.AspNetCore.Mvc;
using SmartGallery.Server.Services.Contracts;

namespace SmartGallery.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationsController(IReservationService service)
        => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetReservations()
        => Ok(await _service.GetReservationsAsync());
}
