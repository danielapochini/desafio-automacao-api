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
        private const string suiteProjeto = "Projetos";
        private const string subSuiteProjeto = "Deletar Projetos Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#8b02bb73-a6d7-0df2-f7a4-a1a2c3b2b06a";

        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Deleta um projeto através de um id válido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void DeletarProjetoDadosValidos()
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
        [AllureDescription("Teste com valor inexistente")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
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
        [AllureDescription("Teste com valor inválido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
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
        [AllureDescription("Teste de campo obrigatório")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
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
