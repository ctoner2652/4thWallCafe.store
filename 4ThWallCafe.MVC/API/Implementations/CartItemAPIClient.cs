using System.Text.Json;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class CartItemAPIClient : ICartItemAPIClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string PATH = "api/CartItem";

        public CartItemAPIClient(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task AdditemAsync(CartItem cartItem)
        {
            var response = await _httpClient.PostAsJsonAsync(PATH, cartItem);
            

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error adding Cart Item: {content}");
            }
        }

        public async Task DeleteUsersCartAsync(Guid userSessionID)
        {
            var response = await _httpClient.DeleteAsync($"{PATH}/{userSessionID}");

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error deleting Users Cart: {content}");
            }
        }

        public async Task<List<CartItem>> GetUsersCartAsync(Guid userSessionID)
        {
            var response = await _httpClient.GetAsync($"{PATH}/{userSessionID}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error getting Cart Items: {content}");
            }
            return JsonSerializer.Deserialize<List<CartItem>>(content, _options);
        }
    }
}
