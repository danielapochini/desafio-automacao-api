using DesafioAutomacaoAPI.Utils.Settings;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
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
            BasicJsonSerializerConfig();
            HeaderAuth();
        }
         

        private void BasicJsonSerializerConfig()
        { 
            //utilizando nugget package de serializer json
            RestClient.UseNewtonsoftJson();

            //setando a configuração do serializador json 
            //para aceitar o modelo de nome dos campos sendo "snake_case"

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
             
        }

        private void HeaderAuth()
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

        private IRestResponse SendRequest(IRestRequest restRequest)
        {
            IRestResponse restResponse = RestClient.Execute(restRequest);
            return restResponse;
        }

        private IRestResponse<T> SendRequest<T>(IRestRequest restRequest) 
        { 
            IRestResponse<T> restResponse = RestClient.Execute<T>(restRequest); 
            return restResponse;
        }

        public IRestResponse PerformDeleteRequest(string url)
        {
            var restRequest = GetRestRequest(url, Method.DELETE);

            return SendRequest(restRequest);
        }

        public IRestResponse<TResponse> PerformDeleteRequest<TResponse>(string url)
        {
            var restRequest = GetRestRequest(url, Method.DELETE);

            return SendRequest<TResponse>(restRequest);
        }

        public IRestResponse<TResponse> PerformGetRequest<TResponse>(string url) 
        {
            var restRequest = GetRestRequest(url, Method.GET); 

            return SendRequest<TResponse>(restRequest);
        }
         
        public IRestResponse<TResponse> PerformPostRequest<TResponse, TBody>(string url, TBody body)
        {
            var restRequest = GetRestRequest(url, body, Method.POST); 

            return SendRequest<TResponse>(restRequest);
        }

        public IRestResponse PerformPostRequest<TBody>(string url, TBody body)
        {
            var restRequest = GetRestRequest(url, body, Method.POST);

            return SendRequest(restRequest);
        }

        public IRestResponse PerformPutRequest(string url)
        {
            var restRequest = GetRestRequest(url, Method.PUT);

            return SendRequest(restRequest);
        }

        public IRestResponse<TResponse> PerformPutRequest<TResponse>(string url)
        {
            var restRequest = GetRestRequest(url, Method.PUT);

            return SendRequest<TResponse>(restRequest);
        }
    }
}
