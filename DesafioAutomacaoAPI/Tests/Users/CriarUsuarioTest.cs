using Allure.Commons;
using Allure.Xunit;
using Allure.Xunit.Attributes; 
using Allure.XUnit;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Model.Request.Users;
using DesafioAutomacaoAPI.Model.Users;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Users;
using DesafioAutomacaoAPI.Utils.Settings;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Users
{
 
    public class CriarUsuarioTest : IAssemblyFixture<TestBase>
    { 
        private static readonly RestManager restManager = new RestManager(); 

        [AllureXunit]
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
        public void CriarUserAcessoInvalido()
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
                criarUsuarioRequest.Data.Message.Should().Be("Invalid access level");
                criarUsuarioRequest.Data.Code.Should().Be(29);
                criarUsuarioRequest.Data.Localized.Should().Be("Invalid value for 'access_level'");
            }
            });

            AllureHelper.AdicionarResultado(criarUsuarioRequest);
        }

        [AllureXunitTheory, CsvData("Utils/Resources/DataDriven/testdata.csv")]
        public void CriarUserDadosValidosDataDriven(string username, string password, string realname, string email, string name, bool enabled, bool isprotected)
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

            Steps.Step("Assertions", () => {
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
    }
}
