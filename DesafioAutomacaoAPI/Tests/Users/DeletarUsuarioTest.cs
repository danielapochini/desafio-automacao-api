using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Users;
using FluentAssertions;
using FluentAssertions.Execution;  
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Users
{ 
    public class DeletarUsuarioTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Usuários";
        private const string subSuiteProjeto = "Deletar Usuários Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#31bdd5d1-15a5-9c2b-8d9f-6b866d1f3260";

        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Deleta um usuário específico através de um id válido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void DeletarUsuarioValido()
        {
            int userId = UsersQueries.ListarUsuarioInativo().Id;

            string urlDeletarUsario = $"api/rest/users/{userId}";

            var deletarUsuarioRequest = restManager.PerformDeleteRequest(urlDeletarUsario);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarUsuarioRequest.StatusCode.Should().Be(204);
                    deletarUsuarioRequest.Content.Should().BeEmpty();
                    deletarUsuarioRequest.StatusDescription.Should().Be("No Content");
                }
            });

            AllureHelper.AdicionarResultado(deletarUsuarioRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valor inválido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void DeletarUsuarioInvalido()
        {
            int codigoEsperado = 29;
            string mensagemEsperada = "Invalid user id";
            string mensagemEsperadaLocalizedString = "Invalid value for 'id'";

            string userId = DadosFakeHelper.GerarString();

            string urlDeletarUsario = $"api/rest/users/{userId}";

            var deletarUsuarioRequest = restManager.PerformDeleteRequest<ErrorMessageResponse>(urlDeletarUsario);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarUsuarioRequest.StatusCode.Should().Be(400);
                    deletarUsuarioRequest.Data.Message.Should().Be(mensagemEsperada);
                    deletarUsuarioRequest.Data.Code.Should().Be(codigoEsperado);
                    deletarUsuarioRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                    deletarUsuarioRequest.StatusDescription.Should().Be(mensagemEsperada);
                }
            });

            AllureHelper.AdicionarResultado(deletarUsuarioRequest);
        }
         
        [AllureXunit]
        [AllureDescription("Teste utilizando único usuário administrador ativo")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void DeletarUnicoAdministradorAtivo()
        {
            int codigoEsperado = 808;
            string mensagemEsperada = "Deleting last administrator not allowed";
            string mensagemEsperadaLocalizedString = "You cannot remove or demote the last administrator account. " +
                        "To perform the action you requested, you first need to create another administrator account.";

            int userId = UsersQueries.ListarAdministrador().Id;

            string urlDeletarUsario = $"api/rest/users/{userId}";

            var deletarUsuarioRequest = restManager.PerformDeleteRequest<ErrorMessageResponse>(urlDeletarUsario);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarUsuarioRequest.StatusCode.Should().Be(400); 
                    deletarUsuarioRequest.StatusDescription.Should().Be(mensagemEsperada);
                    deletarUsuarioRequest.Data.Message.Should().Be(mensagemEsperada);
                    deletarUsuarioRequest.Data.Code.Should().Be(codigoEsperado);
                    deletarUsuarioRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(deletarUsuarioRequest);
        }
    }
}
