using Application.Services;
using Domain.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class ShopController : ControllerBase
{
    private ShopService _shopService;

    public ShopController(ShopService shopService)
    {
        _shopService = shopService;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _shopService.Get(id));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _shopService.Get());
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddShopDto addShop)
    {
        return Ok(await _shopService.Add(addShop));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(EditShopDto editShop, Guid id)
    {
        return Ok(await _shopService.Edit(editShop, id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _shopService.Delete(id);
        return Ok();
    }
}
