using Newtonsoft.Json;
using System.Net;
using System.Text;
using TopShop.Exceptions;
using TopShop.WebApi.Data.Dtos;

namespace TopShop.WebApi.Clients;

public class JsonPlaceholderClient
{
    private readonly IHttpClientFactory _httpClient;

    public JsonPlaceholderClient(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<UserDto>> GetUsers()
    {
        using HttpClient client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://jsonplaceholder.typicode.com/users"));
        var response = await client.SendAsync(request);
        var users = await response.Content.ReadAsAsync<List<UserDto>>();
        return users;
    }

    public async Task<UserDto?> GetUserById(int id)
    {
        using HttpClient client = _httpClient.CreateClient();
        var stringContent = new StringContent(JsonConvert.SerializeObject(new { Id = id }), Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"https://jsonplaceholder.typicode.com/users/{id}"));
        var response = await client.SendAsync(request); 
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new UserNotFoundException();
        }
        else if (!response.IsSuccessStatusCode) 
        {
            throw new Exception("Server error");
        }
        var user = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<UserDto?>(user);

    }

    public async Task<UserDto> CreateUser(UserDto user)
    {
        using HttpClient client = _httpClient.CreateClient();
        var url = new Uri($"https://jsonplaceholder.typicode.com/users/");
        var response = await client.PostAsJsonAsync<UserDto>(url, user);
        return user;
    }
}
