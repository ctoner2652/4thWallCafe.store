namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface IApiClientFactory
    {
        Task<ICategoryAPIClient> CreateCategoryClient();
        Task<IItemAPIClient> CreateItemClient();
        Task<IItemPriceAPIClient> CreateItemPriceAPIClient();
        Task<ITimeOfDayAPIClient> CreateTimeOfDayClient();
        Task<ICafeOrderAPIClient> CreateCafeOrderClient();
        Task<IPaymentTypeAPIClient> CreatePaymentTypeClient();
        Task<ICartItemAPIClient> CreateCartItemClient();
        Task<IOrderItemAPIClient> CreateOrderItemClient();
        Task<string> GetValidTokenAsync();
        Task<string> FetchToken();
        bool TokenIsExpired(string token);
        Task<HttpClient> GetHttpClientWithToken();
        Task<IAPIUserManagement> CreateAPIUserManagement();
    }
}
