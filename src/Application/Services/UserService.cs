using Domain.Data.Dtos;
using Domain.Interfaces;

namespace Application.Services;

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
