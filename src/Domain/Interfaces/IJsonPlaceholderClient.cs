using Domain.Data.Dtos;

namespace Domain.Interfaces;

public interface IJsonPlaceholderClient
{
    Task<JsonPlaceholderResult<UserDto>> CreateUserAsync(UserDto user);
    Task<JsonPlaceholderResult<UserDto>> GetUserByIdAsync(int id);
    Task<List<UserDto>> GetUsersAsync();
}