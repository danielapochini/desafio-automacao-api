using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Users;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Users
{ 
    public class DeletarUsuarioTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
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
        public void DeletarUsuarioInvalido()
        {
            string userId = DadosFakeHelper.GerarString();

            string urlDeletarUsario = $"api/rest/users/{userId}";

            var deletarUsuarioRequest = restManager.PerformDeleteRequest<ErrorMessageResponse>(urlDeletarUsario);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarUsuarioRequest.StatusCode.Should().Be(400);
                    deletarUsuarioRequest.Data.Message.Should().Be("Invalid user id");
                    deletarUsuarioRequest.Data.Code.Should().Be(29);
                    deletarUsuarioRequest.Data.Localized.Should().Be("Invalid value for 'id'");
                    deletarUsuarioRequest.StatusDescription.Should().Be("Invalid user id");
                }
            });

            AllureHelper.AdicionarResultado(deletarUsuarioRequest);
        }

        [AllureXunit]
        public void DeletarUnicoAdministradorAtivo()
        {
            int userId = UsersQueries.ListarAdministrador().Id;

            string urlDeletarUsario = $"api/rest/users/{userId}";

            var deletarUsuarioRequest = restManager.PerformDeleteRequest<ErrorMessageResponse>(urlDeletarUsario);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarUsuarioRequest.StatusCode.Should().Be(400); 
                    deletarUsuarioRequest.StatusDescription.Should().Be("Deleting last administrator not allowed");
                    deletarUsuarioRequest.Data.Message.Should().Be("Deleting last administrator not allowed");
                    deletarUsuarioRequest.Data.Code.Should().Be(808);
                    deletarUsuarioRequest.Data.Localized.Should().Be("You cannot remove or demote the last administrator account. " +
                        "To perform the action you requested, you first need to create another administrator account.");
                }
            });

            AllureHelper.AdicionarResultado(deletarUsuarioRequest);
        }
    }
}
