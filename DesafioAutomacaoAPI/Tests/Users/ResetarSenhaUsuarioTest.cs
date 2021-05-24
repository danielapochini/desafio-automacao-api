using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model; 
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Users;
using FluentAssertions;
using FluentAssertions.Execution;  
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Users
{ 
    public class ResetarSenhaUsuarioTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Usuários";
        private const string subSuiteProjeto = "Restar Senha Usuários Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#31bdd5d1-15a5-9c2b-8d9f-6b866d1f3260";
         
        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Reseta a senha de um usuário específico através de um id válido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void ResetSenhaUsuarioValido()
        { 
            int userId = UsersQueries.ListarInformacoesUsuario("leonardo55").Id;

            string urlResetSenha = $"api/rest/users/{userId}/reset";

            var resetarSenhaRequest = restManager.PerformPutRequest(urlResetSenha);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    resetarSenhaRequest.StatusCode.Should().Be(204);
                    resetarSenhaRequest.Content.Should().BeEmpty();
                    resetarSenhaRequest.StatusDescription.Should().Be("No Content");
                }
            });

            AllureHelper.AdicionarResultado(resetarSenhaRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valor inválido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void ResetSenhaUsuarioInvalido()
        {
            int userId = UsersQueries.ListarUltimoUsuarioCadastrado().Id + 2;

            string mensagemEsperadaLocalizedString = "Invalid value for 'id'";
            string mensagemEsperada = "Invalid user id";
            int codigoEsperado = 29;

            string urlResetSenha = $"api/rest/users/{userId}/reset";

            var resetarSenhaRequest = restManager.PerformPutRequest<ErrorMessageResponse>(urlResetSenha);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    resetarSenhaRequest.StatusCode.Should().Be(400);
                    resetarSenhaRequest.Data.Message.Should().Be(mensagemEsperada);
                    resetarSenhaRequest.Data.Code.Should().Be(codigoEsperado);
                    resetarSenhaRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                    resetarSenhaRequest.StatusDescription.Should().Be(mensagemEsperada);
                }
            });

            AllureHelper.AdicionarResultado(resetarSenhaRequest);
        }

        [AllureXunit]
        [AllureDescription("Reset de senha utilizando único usuário administrador ativo")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void ResetarUnicoAdministradorAtivo()
        {
            int codigoEsperado = 808;
            string mensagemEsperada = "Resetting last administrator not allowed";
            string mensagemEsperadaLocalizedString = "You cannot remove or demote the last administrator account. To perform the action you requested, " +
                        "you first need to create another administrator account.";
             
            int userId = UsersQueries.ListarAdministrador().Id;
             
            string urlResetSenha = $"api/rest/users/{userId}/reset";

            var resetarSenhaRequest = restManager.PerformPutRequest<ErrorMessageResponse>(urlResetSenha);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    resetarSenhaRequest.StatusCode.Should().Be(400);
                    resetarSenhaRequest.StatusDescription.Should().Be(mensagemEsperada);
                    resetarSenhaRequest.Data.Message.Should().Be(mensagemEsperada);
                    resetarSenhaRequest.Data.Code.Should().Be(codigoEsperado);
                    resetarSenhaRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(resetarSenhaRequest);
        }

    }
}
