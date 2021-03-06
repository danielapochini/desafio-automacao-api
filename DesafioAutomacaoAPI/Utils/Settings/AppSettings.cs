using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioAutomacaoAPI.Utils.Settings
{
    public class AppSettings  
    {
        public Uri BaseUrl { get; set; }
        public string Token { get; set; }

        public string ConnectionString { get; set; }

        public AppSettings()
        {
            BaseUrl = new Uri(ReturnParamAppSettings("URL_BASE"));
            Token = ReturnParamAppSettings("TOKEN");
            ConnectionString = ReturnParamAppSettings("CONNECTION_STRING");
        }
         

        public static string ReturnParamAppSettings(string nameParam)
        { 
            
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
#if DEV
               .AddJsonFile($"appsettings.DEV.json", optional: false, reloadOnChange: true)
#endif
#if HML
               .AddJsonFile($"appsettings.HML.json", optional: false, reloadOnChange: true)
#endif
               .AddEnvironmentVariables()
               .Build();

            return config[nameParam].ToString();
        }
    }
}
