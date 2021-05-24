using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Model.Response.Projects;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Projects;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Projects
{
    public class ObterProjetoTest : IAssemblyFixture<TestBase>
    {
        private static readonly RestManager restManager = new RestManager();

        [AllureXunit]
        public void ObterProjetoValorExistente()
        {
            var resultadoListarProjetoBD = ProjectsQueries.ListarUltimoProjetoCadastrado();
            int projetoId = resultadoListarProjetoBD.Id;

            string urlObterProjeto = $"api/rest/projects/{projetoId}";
            var obterProjetoRequest = restManager.PerformGetRequest<GetProjectResponse>(urlObterProjeto);


            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    obterProjetoRequest.StatusCode.Should().Be(200);
                    obterProjetoRequest.StatusDescription.Should().Be("OK"); 
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.Id == resultadoListarProjetoBD.Id);
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.Name == resultadoListarProjetoBD.Name);
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.Description == resultadoListarProjetoBD.Description);
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.Enabled == resultadoListarProjetoBD.Enabled);
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.Status.Id == resultadoListarProjetoBD.Status);
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.ViewState.Id == resultadoListarProjetoBD.ViewState); 
                }
            });

            AllureHelper.AdicionarResultado(obterProjetoRequest);
        }

        [AllureXunit]
        public void ObterProjetoValorInexistente()
        { 
            int projetoId = ProjectsQueries.ListarUltimoProjetoCadastrado().Id + DadosFakeHelper.GerarId(); 
            string mensagemEsperada = $"Project #{projetoId} not found"; 

            string urlObterProjeto = $"api/rest/projects/{projetoId}"; 
            var obterProjetoRequest = restManager.PerformGetRequest<ErrorMessageResponse>(urlObterProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    obterProjetoRequest.StatusCode.Should().Be(404);
                    obterProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    obterProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    obterProjetoRequest.Data.Code.Should().Be(700);
                    obterProjetoRequest.Data.Localized.Should().Be($"Project \"{projetoId}\" not found.");
                }
            });

            AllureHelper.AdicionarResultado(obterProjetoRequest);
        }

        [AllureXunit]
        public void ObterTodosOsProjetos()
        {
            string urlObterProjeto = $"api/rest/projects";
            var obterProjetoRequest = restManager.PerformGetRequest<GetProjectResponse>(urlObterProjeto);


            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    obterProjetoRequest.StatusCode.Should().Be(200);
                    obterProjetoRequest.StatusDescription.Should().Be("OK");
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.Id == 1 && x.Name == "Projeto Mantis API REST" && x.Enabled == true);
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.Id == 2 && x.Name == "Projeto Teste Mantis API REST" && x.Enabled == true);
                }
            });

            AllureHelper.AdicionarResultado(obterProjetoRequest);
        }
    }
}
