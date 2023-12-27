using Microsoft.AspNetCore.Mvc;
using TopShop.WebApi.Data.Dtos;
using TopShop.WebApi.Services;

namespace TopShop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await _userService.GetUsers());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        return Ok(await _userService.GetUserById(id));
    }

    [HttpPost]
    public async Task<ActionResult> Post(UserDto user)
    {
        var userCreated = await _userService.CreateUser(user);
        return Created($"https://jsonplaceholder.typicode.com/users/{userCreated.Id}", userCreated);
    }
}
