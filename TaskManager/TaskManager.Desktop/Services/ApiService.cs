using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace TaskManager.Desktop.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    
    private const string BaseUrl = "http://localhost:5162/api/";

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var loginData = new { Email = email, Password = password };
        var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}Auth/login", loginData);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(content);
            return result?.token;
        }
        return null;
    }
}