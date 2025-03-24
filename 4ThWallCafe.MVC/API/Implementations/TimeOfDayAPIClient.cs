using System.Text.Json;
using _4ThWallCafe.MVC.API.Interfaces;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class TimeOfDayAPIClient : ITimeOfDayAPIClient
    {

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private const string PATH = "api/TimeOfDay";

        public TimeOfDayAPIClient(HttpClient client)
        {
            _httpClient = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<TimeOfDay>> GetAllTimesOfDay()
        {
            var response = await _httpClient.GetAsync(PATH);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Errors getting all TimesOfDay: {content}");
            }
            else
            {
                return JsonSerializer.Deserialize<List<TimeOfDay>>(content, _options);
            }
        }

        public async Task<TimeOfDay> GetTimeOfDayByID(int timeOfDayID)
        {
            var response = await _httpClient.GetAsync($"{PATH}/{timeOfDayID}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error getting Time Of Day: {content}");
            }
            return JsonSerializer.Deserialize<TimeOfDay>(content, _options);
        }
    }
}
