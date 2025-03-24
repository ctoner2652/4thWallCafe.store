using System.Text.Json;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class PaymentTypeAPIClient : IPaymentTypeAPIClient
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string PATH = "api/PaymentType";

        public PaymentTypeAPIClient(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<PaymentType>> GetAllPaymentTypesAsync()
        {
            var response = await _httpClient.GetAsync(PATH);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Errors getting all PayMent Types: {content}");
            }
            else
            {
                return JsonSerializer.Deserialize<List<PaymentType>>(content, _options);
            }
        }

        public async Task<PaymentType> GetPaymentTypeByIDAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{PATH}/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error getting Payment Type: {content}");
            }
            return JsonSerializer.Deserialize<PaymentType>(content, _options);
        }
    }
}
