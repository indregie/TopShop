using TopShop.WebApi.Data.Dtos;

namespace TopShop.WebApi.Interfaces
{
    public interface IJsonPlaceholderClient
    {
        Task<JsonPlaceholderResult<UserDto>> CreateUserAsync(UserDto user);
        Task<JsonPlaceholderResult<UserDto>> GetUserByIdAsync(int id);
        Task<List<UserDto>> GetUsersAsync();
    }
}