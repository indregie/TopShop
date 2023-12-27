using TopShop.Data.Entities;
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

    public async Task<IEnumerable<UserDto>> Get()
    {
        return await _client.GetUsers();
    }
}
