using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Model.Request.Users; 
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers; 
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Users
{ 
    public class CriarUsuarioRegexTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Usuários";
        private const string subSuiteProjeto = "Criar Usuários Regex Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#dd4a3af4-be79-7070-ff9a-e1632c15840e";

        private readonly RestManager restManager = new RestManager(); 

        [AllureXunit]
        [AllureDescription("Criação de usuários utilizando validação Regex para usernames inválidos")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
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
                 
                Steps.Step("Assertions", () =>
                {
                    using (new AssertionScope())
                    {
                        criarUsuarioRequest.StatusCode.Should().Be(400);
                        criarUsuarioRequest.Data.Message.Should().Be($"Invalid username '{userBodyRequest.Username}'");
                        criarUsuarioRequest.Data.Code.Should().Be(805);
                        criarUsuarioRequest.Data.Localized.Should().Be("The username is invalid. Usernames may only contain Latin letters, numbers, spaces, hyphens, dots, plus signs and underscores.");
                    }
                });

                AllureHelper.AdicionarResultado(criarUsuarioRequest);
            } 
        }

        [AllureXunit] 
        [AllureDescription("Criação de usuários utilizando validação Regex para emails inválidos")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
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
                 
                Steps.Step("Assertions", () =>
                {
                    using (new AssertionScope())
                    {
                        criarUsuarioRequest.StatusCode.Should().Be(400);
                        criarUsuarioRequest.Data.Message.Should().Be($"Email '{userBodyRequest.Email}' is invalid.");
                        criarUsuarioRequest.Data.Code.Should().Be(1200);
                        criarUsuarioRequest.Data.Localized.Should().Be("Invalid e-mail address.");
                    }
                });

                AllureHelper.AdicionarResultado(criarUsuarioRequest);
            } 
        }
    }
}
