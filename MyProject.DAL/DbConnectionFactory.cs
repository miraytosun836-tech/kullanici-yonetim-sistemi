using Microsoft.Data.SqlClient;
using System.Configuration;

namespace MyProject.DAL
{
    public static class DbConnectionFactory
    {
        public static SqlConnection Create()
        {
            var settings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (settings == null)
            {
                throw new InvalidOperationException(
                    "App.config içinde DefaultConnection bulunamadı.");
            }

            return new SqlConnection(settings.ConnectionString);
        }
    }
}