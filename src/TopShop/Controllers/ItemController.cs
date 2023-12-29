using Microsoft.AspNetCore.Mvc;
using Domain.Data.Dtos;
using Domain.Services;

namespace TopShop.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private ItemService _itemService;

    public ItemController(ItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _itemService.Get(id));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _itemService.Get());
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddItem addItem)
    {
        return Ok(await _itemService.Add(addItem));
    }

    [HttpPut("{id}")]
    public async Task <IActionResult> Update(EditItem editItem, Guid id)
    {
        return Ok(await _itemService.Edit(editItem, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _itemService.Delete(id);
        return NoContent();
    }

    [HttpPut("{id}/assign-shop")]
    public async Task<IActionResult> AssignToShop([FromBody] AssignToShopDto shop, [FromRoute] Guid id)
    {
        return Ok(await _itemService.AssignToShop(shop, id));
    }

    [HttpPut("{id}/assign-user")]
    public async Task<IActionResult> AssignToUser([FromBody] AssignToUserDto user, [FromRoute] Guid id)
    {
        return Ok(await _itemService.AssignToUser(user, id));
    }
}
