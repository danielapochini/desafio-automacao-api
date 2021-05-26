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
        private const string suiteProjeto = "Projetos";
        private const string subSuiteProjeto = "Obter Projetos Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#6f3e4516-6244-5928-68b4-0f83f2d83943";
         
        private static readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Obtém um projeto com id válido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
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
        [AllureDescription("Obtém todos os projetos acessíveis ao usuário")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
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
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.Id == 1 && x.Name == "Projeto Mantis API REST" && x.Enabled);
                    obterProjetoRequest.Data.Projects.Should().Contain(x => x.Id == 2 && x.Name == "Projeto Teste Mantis API REST" && x.Enabled);
                }
            });

            AllureHelper.AdicionarResultado(obterProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valor inexistente")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void ObterProjetoValorInexistente()
        { 
            int projetoId = DadosFakeHelper.GerarId(); 
            string mensagemEsperada = $"Project #{projetoId} not found";
            string mensagemEsperadaLocalizedString = $"Project \"{projetoId}\" not found.";
            int codigoEsperado = 700;

            string urlObterProjeto = $"api/rest/projects/{projetoId}"; 
            var obterProjetoRequest = restManager.PerformGetRequest<ErrorMessageResponse>(urlObterProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    obterProjetoRequest.StatusCode.Should().Be(404);
                    obterProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    obterProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    obterProjetoRequest.Data.Code.Should().Be(codigoEsperado);
                    obterProjetoRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(obterProjetoRequest);
        }


    }
}
