using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface ICafeOrderAPIClient
    {
        Task<CafeOrder> GetCafeOrderAsync(int id);
        Task<List<CafeOrder>> GetAllCafeOrdersAsync();
        Task EditCafeOrderAsync(CafeOrder cafeOrder);
        Task<CafeOrder> AddCafeOrderAsync(CafeOrder cafeOrder);
    }
}
