using Application.Services;
using Domain.Services;
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
}
