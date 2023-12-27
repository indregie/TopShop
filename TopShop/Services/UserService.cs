using TopShop.Data.Entities;
using TopShop.Exceptions;
using TopShop.WebApi.Clients;
using TopShop.WebApi.Data.Dtos;

namespace TopShop.WebApi.Services;

public class UserService
{
    private readonly JsonPlaceholderClient _client;
    public UserService(JsonPlaceholderClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        return await _client.GetUsers();
    }

    public async Task<UserDto> GetUserById(int id)
    {
        UserDto user = await _client.GetUserById(id) ?? throw new Exception("Failed to deserialize Json.");
        return user;
    }

    public async Task<UserDto> CreateUser(UserDto user)
    {
        UserDto userCreated = await _client.CreateUser(user) ?? throw new Exception();
        return userCreated;
    }
}
