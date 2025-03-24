using _4ThWallCafe.Core.Entities;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface IPaymentTypeAPIClient
    {
        Task<List<PaymentType>> GetAllPaymentTypesAsync();
        Task<PaymentType> GetPaymentTypeByIDAsync(int id);
    }
}
