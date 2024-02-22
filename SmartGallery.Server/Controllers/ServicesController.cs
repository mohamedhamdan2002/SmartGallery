using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Server.Controllers;
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _service;

    public ServicesController(IServiceService service)
        => _service = service;
    [HttpGet]
    public async Task<IActionResult> GetServices()
        => Ok(await _service.GetServicesAsync());

    [HttpGet("{id:int}", Name = "GetServiceById")]
    public async Task<IActionResult> GetService(int id)
        =>  Ok(await _service.GetServiceByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] ServiceForCreationViewModel service)
    {
        if(service is null)
            return BadRequest("ServiceForCreationViewModel object is null");

        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var serviceViewModel = await _service.CreateServiceAsync(service);
        return CreatedAtRoute("GetServiceById", new { serviceViewModel.Id }, serviceViewModel);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateService(int id, [FromBody] ServiceForUpdateViewModel service)
    {
        if(service is null)
            return BadRequest("ServiceForUpdateViewModel object is null");

        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.UpdateServiceAsync(id, service, trackChanges: true);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        await _service.DeleteServiceAsync(id);
        return NoContent();
    }
}
