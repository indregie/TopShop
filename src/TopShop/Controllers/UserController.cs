using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Data.Dtos;

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
    public async Task<IActionResult> Get()
    {
        return Ok(await _userService.Get()) ;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _userService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserDto user)
    {
        var userCreated = await _userService.Create(user);
        return Created($"https://jsonplaceholder.typicode.com/users/{userCreated.Id}", userCreated);
    }
}
