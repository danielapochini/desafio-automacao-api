using DesafioAutomacaoAPI.Utils.Settings;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioAutomacaoAPI.Base
{
    public class RestManager 
    {
        private readonly IRestClient RestClient = new RestClient();

        private readonly AppSettings AppSettings = new AppSettings();

        public RestManager()
        {   
            BasicHeaderAuth();
        }
         

        private void BasicHeaderAuth()
        { 
            RestClient.AddDefaultHeader("Authorization", AppSettings.Token); 
        }
          
        private IRestRequest GetRestRequest(string uri, Method method)
        {
            Uri baseUrl = AppSettings.BaseUrl;
            RestClient.BaseUrl = baseUrl;

            var url = $"{RestClient.BaseUrl}{uri}";

            IRestRequest restRequest = new RestRequest()
            {
                Method = method,
                Resource = url
            };

            return restRequest;
        }

        private IRestRequest GetRestRequest<T>(string uri, T body, Method method)
        {
            Uri baseUrl = AppSettings.BaseUrl;
            RestClient.BaseUrl = baseUrl;
            
            var url = $"{RestClient.BaseUrl}{uri}";

            IRestRequest restRequest = new RestRequest()
            {
                Method = method,
                Resource = url
            };

            if (body != null)
            {
                restRequest.AddJsonBody(body);
            }

            return restRequest;
        }
         
        private IRestResponse<T> SendRequest<T>(IRestRequest restRequest) 
        { 
            IRestResponse<T> restResponse = RestClient.Execute<T>(restRequest); 
            return restResponse;
        }

        public IRestResponse<T> PerformGetRequest<T>(string url) 
        {
            IRestRequest restRequest = GetRestRequest(url, Method.GET);
            IRestResponse<T> restResponse = SendRequest<T>(restRequest);

            return restResponse;
        }
         

    }
}
