namespace _4ThWallCafe.MVC.API.Interfaces
{
    public interface IAPIUserManagement
    {
        Task<string> GenerateToken(string user, string pass); 
    }
}
