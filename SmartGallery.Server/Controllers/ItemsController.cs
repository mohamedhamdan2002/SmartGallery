using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGallery.Server.Services.Contracts;
using SmartGallery.Shared.ViewModels.ItemViewModels;
using SmartGallery.Shared.ViewModels.ServiceViewModels;

namespace SmartGallery.Server.Controllers;

[Route("api/services/{serviceId}/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IItemService _service;

    public ItemsController(IItemService service)
        => _service = service;
    [HttpGet]
    public async Task<IActionResult> GetItemsForService(int serviceId)
        => Ok(await _service.GetItemsForServiceAsync(serviceId));

    //[HttpGet("{id:int}", Name = "GetItemForServiceById")]
    //public async Task<IActionResult> GetItemForServiceById(int serviceId, int id)
    //    => Ok();

    [HttpPost]
    public async Task<IActionResult> CreateItemForService(int serviceId, [FromBody] ItemCreateUpdateViewModel model)
    {
        if (model is null)
            return BadRequest("ItemCreateUpdateViewModel object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var itemViewModel = await _service.CreateItemForServiceAsync(serviceId, model);
        return Ok(itemViewModel);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateItemForService(int serviceId, int id, [FromBody] ItemCreateUpdateViewModel model)
    {
        if (model is null)
            return BadRequest("ItemCreateUpdateViewModel object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.UpdateItemForServiceAsync(serviceId, id, model);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteItemForService(int serviceId, int id)
    {
        await _service.DeleteItemForServiceAsync(serviceId, id);
        return NoContent();
    }
}
