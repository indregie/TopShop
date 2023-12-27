using Microsoft.AspNetCore.Mvc;
using TopShop.Services;
using TopShop.WebApi.Clients;
using TopShop.WebApi.Services;

namespace TopShop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await _userService.Get());
    }
}
