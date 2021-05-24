using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Request.Users;
using DesafioAutomacaoAPI.Model.Users;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Users;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Users
{
    public class CriarUsuarioDataDrivenTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Usuários";
        private const string subSuiteProjeto = "Criar Usuários Data Driven Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#dd4a3af4-be79-7070-ff9a-e1632c15840e";

        private static readonly RestManager restManager = new RestManager();
         
        [AllureXunitTheory, CsvData("Utils/Resources/DataDriven/testdata.csv")]
        [AllureDescription("Criação de usuários utilizando um arquivo .csv")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
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
