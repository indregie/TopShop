using TopShop.Data.Entities;
using TopShop.Exceptions;
using TopShop.WebApi.Clients;
using TopShop.WebApi.Data.Dtos;
using TopShop.WebApi.Interfaces;

namespace TopShop.WebApi.Services;

public class UserService
{
    private readonly IJsonPlaceholderClient _client;
    public UserService(IJsonPlaceholderClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<UserDto>> Get()
    {
        return await _client.GetUsersAsync();
    }

    public async Task<UserDto> GetById(int id)
    {
        JsonPlaceholderResult<UserDto> result = await _client.GetUserByIdAsync(id);

        if (!result.IsSuccessful)
            throw new Exception("user not found");

        return result.Data!;
    }

    public async Task<UserDto> Create(UserDto user)
    {
        JsonPlaceholderResult<UserDto> result = await _client.CreateUserAsync(user);

        if (!result.IsSuccessful)
            throw new Exception("user not found");

        return result.Data!;
    }
}
