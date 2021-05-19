using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Model.Request.Projects;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Projects;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Projects
{
    public class AtualizarProjetoTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        public void AtualizarProjetoIdsIncompativeis()
        {
            int idProjeto = ProjectsQueries.ListarUltimoProjetoCadastrado().Id + 1;
            string urlPatchProjeto = $"api/rest/projects/{idProjeto}";

            var atualizarProjetoBody = new ProjectRequest
            {
                Id = DadosFakeHelper.GerarId(),
                Name = "Teste Atualizar",
                Enabled = true
            };

            var atualizarProjetoRequest = restManager.PerformPatchRequest<ProjectRequest>(urlPatchProjeto, atualizarProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    atualizarProjetoRequest.StatusCode.Should().Be(400);
                    atualizarProjetoRequest.Content.Should().BeEmpty();
                    atualizarProjetoRequest.StatusDescription.Should().Be("Project id mismatch"); 
                }
            });

            AllureHelper.AdicionarResultado(atualizarProjetoRequest);
        }

        [AllureXunit]
        public void AtualizarProjetoIdInexistente()
        {
            int idProjeto = DadosFakeHelper.GerarId();
            string mensagemEsperada = $"Project #{idProjeto} not found";
            string mensagemLocalizedString = $"Project \"{idProjeto}\" not found.";

            string urlPatchProjeto = $"api/rest/projects/{idProjeto}";

            var atualizarProjetoBody = new ProjectRequest
            {
                Id = idProjeto,
                Name = "Teste Atualizar",
                Enabled = true
            };

            var atualizarProjetoRequest = restManager.PerformPatchRequest<ErrorMessageResponse,ProjectRequest>(urlPatchProjeto, atualizarProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    atualizarProjetoRequest.StatusCode.Should().Be(404);
                    atualizarProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    atualizarProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    atualizarProjetoRequest.Data.Code.Should().Be(700);
                    atualizarProjetoRequest.Data.Localized.Should().Be(mensagemLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(atualizarProjetoRequest);
        }
    }
}
