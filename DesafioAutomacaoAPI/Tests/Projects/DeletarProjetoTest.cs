using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Projects;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Projects
{
    public class DeletarProjetoTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        public void DeletarProjetoValorExistente()
        {
            int projetoId = ProjectsQueries.ListarProjetoInativo().Id; 

            string mensagemEsperada = $"Project with id {projetoId} deleted.";

            string urlDeletarProjeto = $"api/rest/projects/{projetoId}";

            var deletarProjetoRequest = restManager.PerformDeleteRequest(urlDeletarProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarProjetoRequest.StatusCode.Should().Be(200);
                    deletarProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    deletarProjetoRequest.Content.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(deletarProjetoRequest);
        }

        [AllureXunit]
        public void DeletarProjetoValorInexistente()
        {
            int projetoId = DadosFakeHelper.GerarId();
            string mensagemEsperada = "Access denied for deleting project.";

            string urlDeletarProjeto = $"api/rest/projects/{projetoId}";

            var deletarProjetoRequest = restManager.PerformDeleteRequest(urlDeletarProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarProjetoRequest.StatusCode.Should().Be(403);
                    deletarProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    deletarProjetoRequest.Content.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(deletarProjetoRequest);
        }


        [AllureXunit]
        public void DeletarProjetoValorInvalido()
        {
            string projetoId = DadosFakeHelper.GerarString();
            string mensagemEsperada = "Invalid project id.";

            string urlDeletarProjeto = $"api/rest/projects/{projetoId}";

            var deletarProjetoRequest = restManager.PerformDeleteRequest(urlDeletarProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarProjetoRequest.StatusCode.Should().Be(400);
                    deletarProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    deletarProjetoRequest.Content.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(deletarProjetoRequest);
        }

        [AllureXunit]
        public void DeletarProjetoValorObrigatorio()
        { 
            string mensagemEsperada = "Mandatory field 'id' is missing.";

            string urlDeletarProjeto = Uri.EscapeUriString("api/rest/projects/ ");

            var deletarProjetoRequest = restManager.PerformDeleteRequest(urlDeletarProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarProjetoRequest.StatusCode.Should().Be(400);
                    deletarProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    deletarProjetoRequest.Content.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(deletarProjetoRequest);
        }
    }
}
