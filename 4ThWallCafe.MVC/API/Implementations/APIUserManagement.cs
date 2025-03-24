using System.Net.Http;
using System.Text;
using System.Text.Json;
using _4ThWallCafe.MVC.API.Interfaces;
using Microsoft.Identity.Client;
using NuGet.Common;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class APIUserManagement : IAPIUserManagement
    {
        private readonly HttpClient _httpClient;
        public APIUserManagement(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<string> GenerateToken(string user, string pass)
        {
            var loginData = new { UserName = user, Password = pass };
            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}auth/login", content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(responseBody);
            return tokenObj["token"];
        }
    }
}
