using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Beautopia.Services
{
    public static class ConnectionStrings
    {
        public static IConfigurationRoot Configuration;

        public static string GetConnectionString(string ConnectionStringName)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            var connectionString = Configuration["ConnectionStrings:" + ConnectionStringName];
            return connectionString;
        }
    }
}
