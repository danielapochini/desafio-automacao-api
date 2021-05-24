using Allure.Xunit;
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
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Users
{ 
    public class CriarUsuarioTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Usuários";
        private const string subSuiteProjeto = "Criar Usuários Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#dd4a3af4-be79-7070-ff9a-e1632c15840e";

        private static readonly RestManager restManager = new RestManager(); 

        [AllureXunit]
        [AllureDescription("Criação de usuários utilizando dados válidos")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void CriarUserDadosValidos()
        {
            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            {
                Username = DadosFakeHelper.GerarNomeDeUsuario(),
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

            var criarUsuarioRequest = restManager.PerformPostRequest<UserResponse, UsersRequest>(urlPostUsuario, userBodyRequest);
            var resultadoListarUsers = UsersQueries.ListarInformacoesUsuario(criarUsuarioRequest.Data.User.Name);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    criarUsuarioRequest.StatusCode.Should().Be(201);
                    resultadoListarUsers.Id.Should().Be(criarUsuarioRequest.Data.User.Id);
                    resultadoListarUsers.UserName.Should().Be(criarUsuarioRequest.Data.User.Name);
                    resultadoListarUsers.RealName.Should().Be(criarUsuarioRequest.Data.User.RealName);
                    resultadoListarUsers.AccessLevel.Should().Be(criarUsuarioRequest.Data.User.AccessLevel.Id);
                    resultadoListarUsers.Email.Should().Be(criarUsuarioRequest.Data.User.Email);
                }
            });

            AllureHelper.AdicionarResultado(criarUsuarioRequest);
        }

        [AllureXunit]
        [AllureDescription("Criação de usuários utilizando um nome de usuário já existente no banco de dados")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void CriarUserNomedeUsuarioExistente()
        {
            string usernameExistente = UsersQueries.ListarUltimoUsuarioCadastrado().UserName;
            string mensagemEsperada = $"Username '{usernameExistente}' already used.";
            string mensagemEsperadaLocalizedString = "That username is already being used. Please go back and select another one.";
            int codigoEsperado = 800; 

            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            {
                Username = usernameExistente,
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
             
            var criarUsuarioRequest = restManager.PerformPostRequest<ErrorMessageResponse, UsersRequest>(urlPostUsuario, userBodyRequest);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    criarUsuarioRequest.StatusCode.Should().Be(400);
                    criarUsuarioRequest.Data.Message.Should().Be(mensagemEsperada);
                    criarUsuarioRequest.Data.Code.Should().Be(codigoEsperado);
                    criarUsuarioRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(criarUsuarioRequest);
        }

        [AllureXunit]
        [AllureDescription("Criação de usuários utilizando um e-mail já existente no banco de dados")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void CriarUserEmailExistente()
        {
            string emailExistente = UsersQueries.ListarUltimoUsuarioCadastrado().Email;
            string mensagemEsperada = $"Email '{emailExistente}' already used.";
            string mensagemEsperadaLocalizedString = "That email is already being used. Please go back and select another one.";
            int codigoEsperado = 813;

            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            {
                Username = DadosFakeHelper.GerarNomeDeUsuario(),
                Password = DadosFakeHelper.GerarSenha(),
                RealName = DadosFakeHelper.GerarNome(),
                Email = emailExistente,
                AccessLevel = new AccessLevelRequest
                {
                    Name = "updater"
                },
                Enabled = true,
                Protected = false
            };

            var criarUsuarioRequest = restManager.PerformPostRequest<ErrorMessageResponse, UsersRequest>(urlPostUsuario, userBodyRequest);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    criarUsuarioRequest.StatusCode.Should().Be(400);
                    criarUsuarioRequest.Data.Message.Should().Be(mensagemEsperada);
                    criarUsuarioRequest.Data.Code.Should().Be(codigoEsperado);
                    criarUsuarioRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(criarUsuarioRequest);
        }

        [AllureXunit]
        [AllureDescription("Criação de usuários utilizando dados inválidos")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void CriarUserAcessoInvalido()
        {
            string mensagemEsperada = "Invalid access level";
            string mensagemEsperadaLocalizedString = "Invalid value for 'access_level'";
            int codigoEsperado = 29;

            string urlPostUsuario = "api/rest/users/";

            var userBodyRequest = new UsersRequest
            { 
                Username = DadosFakeHelper.GerarNomeDeUsuario(),
                Password = DadosFakeHelper.GerarSenha(),
                RealName = DadosFakeHelper.GerarNome(),
                Email = DadosFakeHelper.GerarEmail(),
                AccessLevel = new AccessLevelRequest
                {
                    Name = "aleatorio"
                },
                Enabled = true,
                Protected = false
            };

            var criarUsuarioRequest = restManager.PerformPostRequest<ErrorMessageResponse, UsersRequest>(urlPostUsuario, userBodyRequest);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
            {
                criarUsuarioRequest.StatusCode.Should().Be(400);
                criarUsuarioRequest.Data.Message.Should().Be(mensagemEsperada);
                criarUsuarioRequest.Data.Code.Should().Be(codigoEsperado);
                criarUsuarioRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
            }
            });

            AllureHelper.AdicionarResultado(criarUsuarioRequest);
        } 
    }
}
