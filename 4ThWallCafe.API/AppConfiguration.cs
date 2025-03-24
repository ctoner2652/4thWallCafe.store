using _4ThWallCafe.Core.Interfaces.Application;

namespace _4ThWallCafe.API
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
        public string GetAPIKey()
        {
            return _configuration["Jwt:Key"] ?? "";
        }

        public string GetAPIAudience()
        {
            return _configuration["Jwt:Audience"] ?? "";
        }

        public string GetAPIIssuer()
        {
            return _configuration["Jwt:Issuer"] ?? "";
        }

        public string GetMVCAPIUserName()
        {
            return "";
        }

        public string GetMVCAPIPassword()
        {
            return "";
        }
    }
}
