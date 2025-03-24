using System.Text.Json;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class OrderItemAPIClient : IOrderItemAPIClient
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string PATH = "api/OrderItem";

        public OrderItemAPIClient(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            var response = await _httpClient.PostAsJsonAsync(PATH, orderItem);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error adding Order Item: {content}");
            }
        }

        public async Task EditOrderItemAsync(OrderItem orderItem)
        {
            var response = await _httpClient.PutAsJsonAsync($"{PATH}/{orderItem.OrderItemId}", orderItem);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error editing Order Item: {content}");
            }
        }

        public async Task<List<OrderItem>> GetAllOrderItemsAsync()
        {
            var response = await _httpClient.GetAsync(PATH);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Errors getting all Order Items: {content}");
            }
            else
            {
                return JsonSerializer.Deserialize<List<OrderItem>>(content, _options);
            }
        }

        public async Task<OrderItem> GetOrderItemAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{PATH}/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error getting Order Item: {content}");
            }
            return JsonSerializer.Deserialize<OrderItem>(content, _options);
        }
    }
}
