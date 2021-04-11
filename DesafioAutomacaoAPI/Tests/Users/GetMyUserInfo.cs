using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Users; 
using DesafioAutomacaoAPI.Utils.Settings; 
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit; 

namespace DesafioAutomacaoAPI.Tests.Users
{ 
    public class GetMyUserInfo  
    {
        private readonly RestManager restManager = new RestManager();

        [Fact]
        public void TestGetMyUserInfo()
        {
            string urlGetUserInfo = "api/rest/users/me";
            
            IRestResponse<UsersModel> obterDados = restManager.PerformGetRequest<UsersModel>(urlGetUserInfo);
             
            Assert.Equal(200, (int)obterDados.StatusCode);
            Assert.Equal("administrator", obterDados.Data.Name);
            Assert.Equal("administrator", obterDados.Data.AccessLevel.Name);
        }
    }
}
