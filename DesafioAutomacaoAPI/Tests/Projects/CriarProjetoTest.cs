using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Request.Projects;
using DesafioAutomacaoAPI.Model.Response.Projects; 
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Projects;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Projects
{ 
    public class CriarProjetoTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Projetos";
        private const string subSuiteProjeto = "Criar Projetos Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#16677869-82ac-50a0-e69f-c33986fbcf5f";

        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Cria um projeto válido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void CriarProjetoDadosValidos()
        {
            int idProjeto = ProjectsQueries.ListarUltimoProjetoCadastrado().Id + 1;
            string urlPostProjeto = $"api/rest/projects/";

            var criarProjetoBody = new ProjectRequest
            {
                Id = idProjeto,
                Name = "Projeto Automação Mantis API REST", 
                Status = new Model.Request.Projects.Status
                {
                    Id = 10,
                    Name = "development",
                    Label = "development"
                },
                Description = "Criação de Projeto Mantis",
                Enabled = true,
                FilePath = "/tmp/",
                ViewState = new Model.Request.Projects.ViewState
                {
                    Id = 10,
                    Name = "public",
                    Label = "public"
                }
            };

            var criarProjetoRequest = restManager.PerformPostRequest<ProjectResponse, ProjectRequest>(urlPostProjeto, criarProjetoBody);

            var resultadoListarProjetoBD = ProjectsQueries.ListarInformacoesProjeto(criarProjetoRequest.Data.Project.Id);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    criarProjetoRequest.StatusCode.Should().Be(201); 
                    criarProjetoRequest.StatusDescription.Should().Be($"Project created with id {criarProjetoBody.Id}");
                    resultadoListarProjetoBD.Id.Should().Be(criarProjetoRequest.Data.Project.Id);
                    resultadoListarProjetoBD.Name.Should().Be(criarProjetoRequest.Data.Project.Name);
                    resultadoListarProjetoBD.Status.Should().Be(criarProjetoRequest.Data.Project.Status.Id);
                    resultadoListarProjetoBD.Enabled.Should().Be(criarProjetoRequest.Data.Project.Enabled);
                    resultadoListarProjetoBD.ViewState.Should().Be(criarProjetoRequest.Data.Project.ViewState.Id);
                    resultadoListarProjetoBD.Description.Should().Be(criarProjetoRequest.Data.Project.Description);
                }
            });

            AllureHelper.AdicionarResultado(criarProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valores inválidos")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void CriarProjetoDadosInvalidos()
        {
            int idProjeto = ProjectsQueries.ListarUltimoProjetoCadastrado().Id + 1;
            string urlPostProjeto = $"api/rest/projects/";

            var criarProjetoBody = new ProjectRequest
            {
                Id = idProjeto,
                Name = "",
                Status = new Model.Request.Projects.Status
                {
                    Id = 10,
                    Name = "development",
                    Label = "development"
                },
                Description = "",
                Enabled = true,
                FilePath = "/tmp/",
                ViewState = new Model.Request.Projects.ViewState
                {
                    Id = 10,
                    Name = "public",
                    Label = "public"
                }
            };

            var criarProjetoRequest = restManager.PerformPostRequest<ProjectResponse, ProjectRequest>(urlPostProjeto, criarProjetoBody);
 
            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    criarProjetoRequest.StatusCode.Should().Be(500);
                    criarProjetoRequest.StatusDescription.Should().Be("Internal Server Error"); 
                }
            });

            AllureHelper.AdicionarResultado(criarProjetoRequest);
        }
    }
}
