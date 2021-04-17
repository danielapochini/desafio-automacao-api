using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesafioAutomacaoAPI.Utils.Settings
{
    public class AppSettings : LaunchSettingsFixture
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
            string environmentName = GetEnvironment();
            
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true) 
               .Build();

            return config[nameParam].ToString();
        }
    }
}
