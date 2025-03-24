using System.Text.Json;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class CafeOrderAPIClient : ICafeOrderAPIClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string PATH = "api/CafeOrder";

        public CafeOrderAPIClient(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<CafeOrder> AddCafeOrderAsync(CafeOrder cafeOrder)
        {
            var response = await _httpClient.PostAsJsonAsync(PATH, cafeOrder);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error adding Cafe Order: {content}");
            }

            return JsonSerializer.Deserialize<CafeOrder>(content, _options);
        }

        public async Task EditCafeOrderAsync(CafeOrder cafeOrder)
        {
            var response = await _httpClient.PutAsJsonAsync($"{PATH}/{cafeOrder.OrderId}", cafeOrder);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error editing Cafe Order : {content}");
            }
        }

        public async Task<List<CafeOrder>> GetAllCafeOrdersAsync()
        {
            var response = await _httpClient.GetAsync(PATH);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Errors getting all Cafe Orders: {content}");
            }
            else
            {
                return JsonSerializer.Deserialize<List<CafeOrder>>(content, _options);
            }
        }

        public async Task<CafeOrder> GetCafeOrderAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{PATH}/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error getting Cafe Order: {content}");
            }
            return JsonSerializer.Deserialize<CafeOrder>(content, _options);
        }
    }
}
