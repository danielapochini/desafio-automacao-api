using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Request.Users;
using DesafioAutomacaoAPI.Model.Users;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Users;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DesafioAutomacaoAPI.Tests.Users
{
    public class CriarUsuarioRegexTest
    {
        private readonly RestManager restManager = new RestManager();

        [Fact]
        public void CriarUserEmaiValidoRegex()
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

            var validatorEmail = RegexHelper.IsValidAddress(userBodyRequest.Email);

            if (validatorEmail == true)
            {
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
            else
            {
                Assert.False(validatorEmail, "E-mail não é valido!");
            }


        }
        [Fact]
        public void CriarUserEmailnvalidoRegex()
        {
            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            {
                Username = GerarDadosFake.GerarNomeDeUsuario(),
                Password = GerarDadosFake.GerarSenha(),
                RealName = GerarDadosFake.GerarNome(),
                Email = "email@valido",
                AccessLevel = new AccessLevelRequest
                {
                    Name = "updater"
                },
                Enabled = true,
                Protected = false
            };

            var validatorEmail = RegexHelper.IsValidAddress(userBodyRequest.Email);

            if (validatorEmail == true)
            {
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
            else
            {
                Assert.False(validatorEmail, "E-mail não é valido!");
            }
        }
    }
}
