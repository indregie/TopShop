using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using TopShop.Exceptions;
using TopShop.WebApi.Data.Dtos;
using TopShop.WebApi.Interfaces;

namespace TopShop.WebApi.Clients;

public class JsonPlaceholderClient : IJsonPlaceholderClient
{
    private readonly HttpClient _httpClient;

    public JsonPlaceholderClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
        var users = await response.Content.ReadAsAsync<List<UserDto>>();
        return users;
    }

    public async Task<JsonPlaceholderResult<UserDto>> GetUserByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsAsync<UserDto>();

            return new JsonPlaceholderResult<UserDto>
            {
                Data = data,
                IsSuccessful = true,
                ErrorMessage = ""
            };
        }
        else
        {
            return new JsonPlaceholderResult<UserDto>
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString()
            };
        }
    }

    public async Task<JsonPlaceholderResult<UserDto>> CreateUserAsync(UserDto user)
    {
        var response = await _httpClient.PostAsJsonAsync<UserDto>($"https://jsonplaceholder.typicode.com/users/", user);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsAsync<UserDto>();

            return new JsonPlaceholderResult<UserDto>
            {
                Data = data,
                IsSuccessful = true,
                ErrorMessage = ""
            };
        }
        else
        {
            return new JsonPlaceholderResult<UserDto>
            {
                IsSuccessful = false,
                ErrorMessage = response.StatusCode.ToString()
            };
        }
    }
}
