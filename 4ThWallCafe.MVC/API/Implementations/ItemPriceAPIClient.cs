using System.Text.Json;
using _4ThWallCafe.API.Model;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class ItemPriceAPIClient : IItemPriceAPIClient
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string PATH = "api/ItemPrice";

        public ItemPriceAPIClient(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }


        public async Task AddItemPrice(AddItemPrice itemPrice)
        {
            var response = await _httpClient.PostAsJsonAsync(PATH, itemPrice);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error adding ItemPrice: {content}");
            }
        }

        public async Task EditItemPrice(EditItemPrice itemPrice)
        {
            var response = await _httpClient.PutAsJsonAsync($"{PATH}/{itemPrice.ItemPriceID}", itemPrice);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error editing ItemPrice: {content}");
            }
        }

        public async Task<List<ItemPrice>> GetAllActiveItemPrices()
        {
            var response = await _httpClient.GetAsync($"{PATH}/active");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Errors getting all ItemPrices: {content}");
            }
            else
            {
                return JsonSerializer.Deserialize<List<ItemPrice>>(content, _options);
            }
        }

        public async Task<List<ItemPrice>> GetAllItemPrices()
        {
            var response = await _httpClient.GetAsync($"{PATH}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Errors getting all ItemPrices: {content}");
            }
            else
            {
                return JsonSerializer.Deserialize<List<ItemPrice>>(content, _options);
            }
        }

        public async Task<ItemPrice> GetItemPriceByID(int itemPriceID)
        {
            var response = await _httpClient.GetAsync($"{PATH}/{itemPriceID}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error getting ItemPrice: {content}");
            }
            return JsonSerializer.Deserialize<ItemPrice>(content, _options);
        }
    }
}
