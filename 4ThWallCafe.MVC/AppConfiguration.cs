using _4ThWallCafe.Core.Interfaces.Application;

namespace _4ThWallCafe.MVC
{
    public class AppConfiguration : IApplicationConfiguration
    {
        private IConfiguration _configuration;

        public AppConfiguration()
        {
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true)
            .Build();
        }
        public string GetConnectionString()
        {
            return _configuration["CafeDB"] ?? "";
        }

        public string GetBaseAddress()
        {
            return _configuration["BaseAddress"] ?? "";
        }

        public string GetMVCAPIUserName()
        {
            return _configuration["MVCAPIUserName"] ?? "";
        }

        public string GetMVCAPIPassword()
        {
            return _configuration["MVCAPIPassword"] ?? "";
        }

        public string GetAPIKey()
        {
            return "";
        }

        public string GetAPIAudience()
        {
            return "";
        }

        public string GetAPIIssuer()
        {
            return "";
        }
    }
}
