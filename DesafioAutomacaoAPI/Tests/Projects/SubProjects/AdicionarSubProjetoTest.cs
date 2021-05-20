using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Model.Request.Projects;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Queries.Projects;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Projects.SubProjects
{
    public class AdicionarSubProjetoTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager();
        private readonly ProjectsEntities subProjeto = ProjectsQueries.ListarUltimoProjetoCadastrado();

        [AllureXunit]
        public void AdicionarSubProjetoIdProjetoValido()
        { 
            var projetoPai = ProjectsQueries.ListarInformacoesProjeto("Projeto Mantis API REST");

            string mensagemEsperada = $"Subproject '{subProjeto.Id}' added to project '{projetoPai.Id}'";

            string urlPostSubProjeto = $"api/rest/projects/{projetoPai.Id}/subprojects";

            var adicionarSubProjetoBody = new SubProjectRequest
            {
                Project = new ProjectAttribute
                {
                    Name = subProjeto.Name
                },
                InheritParent = true
            };

            var adicionarSubProjetoRequest = restManager.PerformPostRequest<SubProjectRequest>(urlPostSubProjeto, adicionarSubProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    adicionarSubProjetoRequest.Content.Should().BeEmpty();
                    adicionarSubProjetoRequest.StatusCode.Should().Be(204);
                    adicionarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada); 
                }
            });

        }

        [AllureXunit]
        public void AdicionarSubProjetoIdProjetoInvalido()
        { 
            string projetoPaiId = DadosFakeHelper.GerarString();

            string mensagemEsperada = "'project_id' must be numeric";

            string urlPostSubProjeto = $"api/rest/projects/{projetoPaiId}/subprojects";

            var adicionarSubProjetoBody = new SubProjectRequest
            {
                Project = new ProjectAttribute
                {
                    Name = subProjeto.Name
                },
                InheritParent = true
            };

            var adicionarSubProjetoRequest = restManager.PerformPostRequest<ErrorMessageResponse,SubProjectRequest>(urlPostSubProjeto, adicionarSubProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    adicionarSubProjetoRequest.StatusCode.Should().Be(400);
                    adicionarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    adicionarSubProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    adicionarSubProjetoRequest.Data.Code.Should().Be(29);
                    adicionarSubProjetoRequest.Data.Localized.Should().Be("Invalid value for 'project_id'");
                }
            });

        }

        [AllureXunit]
        public void AdicionarSubProjetoIdProjetoInexistente()
        { 
            int projetoPaiId = subProjeto.Id + DadosFakeHelper.GerarId();

            string mensagemEsperada = $"Project '{projetoPaiId}' not found";
            string mensagemEsperadaLocalized = $"Project \"{projetoPaiId}\" not found.";

            string urlPostSubProjeto = $"api/rest/projects/{projetoPaiId}/subprojects";

            var adicionarSubProjetoBody = new SubProjectRequest
            {
                Project = new ProjectAttribute
                {
                    Name = subProjeto.Name
                },
                InheritParent = true
            };

            var adicionarSubProjetoRequest = restManager.PerformPostRequest<ErrorMessageResponse, SubProjectRequest>(urlPostSubProjeto, adicionarSubProjetoBody);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    adicionarSubProjetoRequest.StatusCode.Should().Be(404);
                    adicionarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    adicionarSubProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    adicionarSubProjetoRequest.Data.Code.Should().Be(700);
                    adicionarSubProjetoRequest.Data.Localized.Should().Be(mensagemEsperadaLocalized);
                }
            });

        }
    }
}
