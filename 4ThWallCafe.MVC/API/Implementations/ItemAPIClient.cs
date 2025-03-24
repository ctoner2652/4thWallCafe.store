using System.Text.Json;
using _4ThWallCafe.API.Model;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class ItemAPIClient : IItemAPIClient
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string PATH = "api/Item";

        public ItemAPIClient(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task AddItem(AddItem item)
        {
            var response = await _httpClient.PostAsJsonAsync(PATH, item);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error adding Item: {content}");
            }
        }

        public async Task EditItem(EditItem item)
        {
            var response = await _httpClient.PutAsJsonAsync($"{PATH}/{item.ItemId}", item);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error editing Item: {content}");
            }
        }

        public async Task<Item> GetItemByID(int id)
        {
            var response = await _httpClient.GetAsync($"{PATH}/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error getting item: {content}");
            }
            return JsonSerializer.Deserialize<Item>(content, _options);
        }

        public async Task<List<Item>> GetItemsByCategory(int categoryID)
        {
            var response = await _httpClient.GetAsync($"{PATH}/category/{categoryID}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Errors getting all Items for category ID {categoryID} : {content}");
            }
            else
            {
                return JsonSerializer.Deserialize<List<Item>>(content, _options);
            }
        }

        public async Task<List<Item>> GetAllItems()
        {
            var response = await _httpClient.GetAsync(PATH);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Errors getting all Items: {content}");
            }
            else
            {
                return JsonSerializer.Deserialize<List<Item>>(content, _options);
            }
        }
    }
}
