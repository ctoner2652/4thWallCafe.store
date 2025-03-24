using System.Text.Json;
using _4ThWallCafe.API.Model;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class CategoryAPIClient : ICategoryAPIClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string PATH = "api/Category";

        public CategoryAPIClient(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task CreateCategoryAsync(AddCategory category)
        {
            var response = await _httpClient.PostAsJsonAsync(PATH, category);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error adding Category: {content}");
            }
          
        }

        public async Task EditCategoryAsync(EditCategory category)
        {
            var response = await _httpClient.PutAsJsonAsync($"{PATH}/{category.CategoryID}", category);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error editing Category: {content}");
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync(PATH);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Errors getting all Categories: {content}");
            }
            else
            {
                return JsonSerializer.Deserialize<List<Category>>(content, _options);
            }
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{PATH}/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error getting category: {content}");
            }
            return JsonSerializer.Deserialize<Category>(content, _options);
        }
    }
}
