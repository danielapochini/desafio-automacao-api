using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioAutomacaoAPI.Utils.Settings
{
    public class AppSettings
    {
        public Uri BaseUrl { get; set; }
        public string Token { get; set; }

        public AppSettings()
        {
            BaseUrl = new Uri(ReturnParamAppSettings("URL_BASE"));
            Token = ReturnParamAppSettings("TOKEN");
        }

        public static string ReturnParamAppSettings(string nameParam)
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            return config[nameParam].ToString();
        }
    }
}
