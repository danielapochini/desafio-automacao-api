using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Users;
using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Queries.Users;
using DesafioAutomacaoAPI.Utils.Settings;
using FluentAssertions;
using System.Linq;
using FluentAssertions.Execution;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit; 

namespace DesafioAutomacaoAPI.Tests.Users
{ 
    public class ObterInfosMeuUsuarioTest  
    {
        private readonly RestManager restManager = new RestManager();

        [Fact]
        public void ObterMyUserInfo()
        { 
            string urlGetUserInfo = "api/rest/users/me";
             
            var obterDados = restManager.PerformGetRequest<UsersResponseAttributes>(urlGetUserInfo);             
            var resultadoListarUsers = UsersQueries.ListarInformacoesUsuario(obterDados.Data.Name);
             
            //usando fluent validations para realizar asserts multiplos 
            using (new AssertionScope())
            {
                obterDados.StatusCode.Should().Be(200); 
                resultadoListarUsers.Id.Should().Be(obterDados.Data.Id);
                resultadoListarUsers.UserName.Should().Be(obterDados.Data.Name);
                resultadoListarUsers.RealName.Should().Be(obterDados.Data.RealName);
                resultadoListarUsers.Email.Should().Be(obterDados.Data.Email);
                resultadoListarUsers.AccessLevel.Should().Be(obterDados.Data.AccessLevel.Id);
            }
        }
    }
}
