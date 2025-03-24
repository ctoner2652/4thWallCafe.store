using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using _4ThWallCafe.Core.Interfaces.Application;
using _4ThWallCafe.MVC.API.Interfaces;
using Microsoft.Identity.Client;

namespace _4ThWallCafe.MVC.API.Implementations
{
    public class APIClientFactory : IApiClientFactory
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationConfiguration _appConfig;
        public APIClientFactory(HttpClient client, IHttpContextAccessor httpContextAccessor, IApplicationConfiguration appConfig)
        {
            _httpClient = client;
            _httpContextAccessor = httpContextAccessor;
            _appConfig = appConfig;
        }

        public async Task<string> GetValidTokenAsync()
        {
            var token = await FetchToken();
            return token;
        }
        public async Task<string> FetchToken()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var token = session.GetString("ApiToken");
            if (string.IsNullOrEmpty(token))
            {
                var cookies = _httpContextAccessor.HttpContext?.Request.Cookies;
                if (cookies != null)
                {
                    token = cookies["ApiToken"];
                }
            }
            if(string.IsNullOrEmpty(token) || TokenIsExpired(token))
            {
                var loginData = new { UserName = _appConfig.GetMVCAPIUserName(), Password = _appConfig.GetMVCAPIPassword() };
                var json = JsonSerializer.Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}auth/login", content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenObj = JsonSerializer.Deserialize<Dictionary<string, string>>(responseBody);
                token = tokenObj["token"];
                session.SetString("ApiToken", token);
                var responseCookies = _httpContextAccessor.HttpContext?.Response.Cookies;
                responseCookies?.Append("ApiToken", token, new CookieOptions
                {
                    HttpOnly = true, 
                    Secure = true, 
                    IsEssential = true, 
                    Expires = DateTime.UtcNow.AddDays(5) 
                });
            }
            return token;
        }
        public bool TokenIsExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                return true; // Invalid token format
            }

            var jwtToken = tokenHandler.ReadJwtToken(token);
            var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);

            if (expClaim == null) return true; // No expiration claim, invalid token

            var expUnix = long.Parse(expClaim.Value);
            var expDate = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;

            return expDate < DateTime.UtcNow.AddMinutes(1); // Expired or about to expire
        }

        public async Task<HttpClient> GetHttpClientWithToken()
        {
            var token = await GetValidTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return _httpClient;
        }

        public async Task<ICafeOrderAPIClient> CreateCafeOrderClient()
        {
            return new CafeOrderAPIClient(await GetHttpClientWithToken());
        }

        public async Task<ICartItemAPIClient> CreateCartItemClient()
        {
            return new CartItemAPIClient(await GetHttpClientWithToken());
        }

        public async Task<ICategoryAPIClient> CreateCategoryClient()
        {
            return new CategoryAPIClient(await GetHttpClientWithToken());
        }

        public async Task<IItemAPIClient> CreateItemClient()
        {
            return new ItemAPIClient(await GetHttpClientWithToken());
        }

        public async Task<IItemPriceAPIClient> CreateItemPriceAPIClient()
        {
            return new ItemPriceAPIClient(await GetHttpClientWithToken());
        }

        public async Task<IOrderItemAPIClient> CreateOrderItemClient()
        {
            return new OrderItemAPIClient(await GetHttpClientWithToken());
        }

        public async Task<IPaymentTypeAPIClient> CreatePaymentTypeClient()
        {
            return new PaymentTypeAPIClient(await GetHttpClientWithToken());
        }

        public async Task<ITimeOfDayAPIClient> CreateTimeOfDayClient()
        {
            return new TimeOfDayAPIClient(await GetHttpClientWithToken());
        }
        public async Task<IAPIUserManagement> CreateAPIUserManagement()
        {
            return new APIUserManagement(await GetHttpClientWithToken());
        }
    }
}
