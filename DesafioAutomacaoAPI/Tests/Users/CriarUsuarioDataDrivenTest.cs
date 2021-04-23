using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Model.Request.Users;
using DesafioAutomacaoAPI.Model.Users;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Users;
using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DesafioAutomacaoAPI.Tests.Users
{
    public class CriarUsuarioDataDrivenTest
    {
        private readonly RestManager restManager = new RestManager();

        [AllureXunitTheory]
        [CsvData("Utils/Resources/DataDriven/testdata.csv")]
        public void CriarUserDadosValidos(string username, string password, string realname, string email, string name, bool enabled, bool isprotected)
        {
            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            {
                Username = username,
                Password = password,
                RealName = realname,
                Email = email,
                AccessLevel = new AccessLevelRequest
                {
                    Name = name
                },
                Enabled = enabled,
                Protected = isprotected
            };
 
            var criarUsuarioRequest = restManager.PerformPostRequest<UserResponse, UsersRequest>(urlPostUsuario, userBodyRequest);
            var resultadoListarUsers = UsersQueries.ListarInformacoesUsuario(criarUsuarioRequest.Data.User.Name);

            using (new AssertionScope())
            {
                criarUsuarioRequest.StatusCode.Should().Be(201);
                resultadoListarUsers.Id.Should().Be(criarUsuarioRequest.Data.User.Id);
                resultadoListarUsers.UserName.Should().Be(criarUsuarioRequest.Data.User.Name);
                resultadoListarUsers.RealName.Should().Be(criarUsuarioRequest.Data.User.RealName);
                resultadoListarUsers.AccessLevel.Should().Be(criarUsuarioRequest.Data.User.AccessLevel.Id);
                resultadoListarUsers.Email.Should().Be(criarUsuarioRequest.Data.User.Email);
            }
        }
         
    }
}
