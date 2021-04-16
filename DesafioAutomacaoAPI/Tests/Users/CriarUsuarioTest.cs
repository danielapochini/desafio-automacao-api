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
    public class CriarUsuarioTest
    {
        private readonly RestManager restManager = new RestManager();

        [Fact]
        public void CriarUserDadosValidos()
        {
            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            {
                Username = GerarDadosFake.GerarNomeDeUsuario(),
                Password = GerarDadosFake.GerarSenha(),
                RealName = GerarDadosFake.GerarNome(),
                Email = GerarDadosFake.GerarEmail(),
                AccessLevel = new AccessLevelRequest
                {
                    Name = "updater"
                },
                Enabled = true,
                Protected = false
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

        [Fact]
        public void CriarUserAcessoInvalido()
        {
            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            { 
                Username = GerarDadosFake.GerarNomeDeUsuario(),
                Password = GerarDadosFake.GerarSenha(),
                RealName = GerarDadosFake.GerarNome(),
                Email = GerarDadosFake.GerarEmail(),
                AccessLevel = new AccessLevelRequest
                {
                    Name = "aleatorio"
                },
                Enabled = true,
                Protected = false
            };

            var criarUsuarioRequest = restManager.PerformPostRequest<ErrorMessageResponse, UsersRequest>(urlPostUsuario, userBodyRequest);

            using (new AssertionScope())
            {
                criarUsuarioRequest.StatusCode.Should().Be(400);
                criarUsuarioRequest.Data.Message.Should().Be("Invalid access level");
                criarUsuarioRequest.Data.Code.Should().Be(29);
                criarUsuarioRequest.Data.Localized.Should().Be("Invalid value for 'access_level'"); 
            } 
        }

    }
}
