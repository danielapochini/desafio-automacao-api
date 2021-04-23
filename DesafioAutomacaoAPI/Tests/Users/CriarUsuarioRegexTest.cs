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

        [AllureXunit]
        public void CriarUserInvalidoUsernameRegex()
        {
            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            {
                Username = "!testeusername",
                Password = DadosFakeHelper.GerarSenha(),
                RealName = DadosFakeHelper.GerarNome(),
                Email = DadosFakeHelper.GerarEmail(),
                AccessLevel = new AccessLevelRequest
                {
                    Name = "updater"
                },
                Enabled = true,
                Protected = false
            };

            var validatorUsername = RegexHelper.IsValidUsername(userBodyRequest.Username);

            if (validatorUsername == false)
            {
                var criarUsuarioRequest = restManager.PerformPostRequest<ErrorMessageResponse, UsersRequest>(urlPostUsuario, userBodyRequest);

                using (new AssertionScope())
                {
                    criarUsuarioRequest.StatusCode.Should().Be(400);
                    criarUsuarioRequest.Data.Message.Should().Be($"Invalid username '{userBodyRequest.Username}'");
                    criarUsuarioRequest.Data.Code.Should().Be(805);
                    criarUsuarioRequest.Data.Localized.Should().Be("The username is invalid. Usernames may only contain Latin letters, numbers, spaces, hyphens, dots, plus signs and underscores.");
                }
            }
            


        }
        [AllureXunit]
        public void CriarUserInvalidoEmailRegex()
        {
            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            {
                Username = DadosFakeHelper.GerarNomeDeUsuario(),
                Password = DadosFakeHelper.GerarSenha(),
                RealName = DadosFakeHelper.GerarNome(),
                Email = "teste@@com",
                AccessLevel = new AccessLevelRequest
                {
                    Name = "updater"
                },
                Enabled = true,
                Protected = false
            };

            var validatorEmail = RegexHelper.IsValidAddress(userBodyRequest.Email);

            if (validatorEmail == false)
            {
                var criarUsuarioRequest = restManager.PerformPostRequest<ErrorMessageResponse, UsersRequest>(urlPostUsuario, userBodyRequest);

                using (new AssertionScope())
                {
                    criarUsuarioRequest.StatusCode.Should().Be(400);
                    criarUsuarioRequest.Data.Message.Should().Be($"Email '{userBodyRequest.Email}' is invalid.");
                    criarUsuarioRequest.Data.Code.Should().Be(1200);
                    criarUsuarioRequest.Data.Localized.Should().Be("Invalid e-mail address.");
                }
            } 
        }
    }
}
