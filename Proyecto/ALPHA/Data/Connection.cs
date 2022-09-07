using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ALPHA.Data
{
    public class Connection

    {
        private string cadenaSQL = string.Empty;
        public Connection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;
        }

        public string getCadenaSQL()
        {
            return cadenaSQL;
        }
    }

}
