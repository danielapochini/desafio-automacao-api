using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Entities;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Projects;
using FluentAssertions;
using FluentAssertions.Execution;
using System; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Projects.SubProjects
{ 
    public class DeletarSubProjetoTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "SubProjetos";
        private const string subSuiteProjeto = "Deletar SubProjetos Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#941a1e99-6b8f-4ced-ef3f-ee1bf6595a0c";

        private readonly SubProjectsEntities informacoesBD = SubProjectsQueries.ListarUltimoSubProjetoCadastrado();

        private static readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Deleta um subprojeto válido de um projeto pai")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void DeletarSubProjetoValorExistente()
        {
            int idProjeto = informacoesBD.ParentId;
            int idSubProjeto = informacoesBD.ChildId;

            string mensagemEsperada = $"Subproject '{idSubProjeto}' deleted from project '{idProjeto}'";

            string urlDeletarSubProjeto = $"api/rest/projects/{idProjeto}/subprojects/{idSubProjeto}";

            var deletarSubProjetoRequest = restManager.PerformDeleteRequest(urlDeletarSubProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarSubProjetoRequest.StatusCode.Should().Be(204);
                    deletarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    deletarSubProjetoRequest.Content.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(deletarSubProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valor inexistente")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void DeletarSubProjetoValorInexistente()
        {
            int idProjeto = informacoesBD.ParentId;
            int idSubProjeto = DadosFakeHelper.GerarId();

            string mensagemEsperada = $"Project '{idSubProjeto}' not found";
            string mensagemEsperadaLocalizedString = $"Project \"{idSubProjeto}\" not found.";
            int codigoEsperado = 700;

            string urlDeletarSubProjeto = $"api/rest/projects/{idProjeto}/subprojects/{idSubProjeto}";

            var deletarSubProjetoRequest = restManager.PerformDeleteRequest<ErrorMessageResponse>(urlDeletarSubProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarSubProjetoRequest.StatusCode.Should().Be(404);
                    deletarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    deletarSubProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    deletarSubProjetoRequest.Data.Code.Should().Be(codigoEsperado);
                    deletarSubProjetoRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(deletarSubProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com projeto inválido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void DeletarSubProjetoValorInvalido()
        {
            int idProjeto = informacoesBD.ParentId;
            string idSubProjeto = DadosFakeHelper.GerarString();

            string mensagemEsperada = "'subproject_id' must be numeric";
            string mensagemEsperadaLocalizedString = "Invalid value for 'subproject_id'";
            int codigoEsperado = 29;

            string urlDeletarSubProjeto = $"api/rest/projects/{idProjeto}/subprojects/{idSubProjeto}";

            var deletarSubProjetoRequest = restManager.PerformDeleteRequest<ErrorMessageResponse>(urlDeletarSubProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarSubProjetoRequest.StatusCode.Should().Be(400); 
                    deletarSubProjetoRequest.Data.Message.Should().Be(mensagemEsperada);
                    deletarSubProjetoRequest.Data.Code.Should().Be(codigoEsperado);
                    deletarSubProjetoRequest.Data.Localized.Should().Be(mensagemEsperadaLocalizedString);
                }
            });

            AllureHelper.AdicionarResultado(deletarSubProjetoRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valor obrigatório")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void DeletarSubProjetoValorObrigatorio()
        {
            int idProjeto = informacoesBD.ParentId;

            string mensagemEsperada = "Subproject id is missing."; 

            string urlDeletarSubProjeto = Uri.EscapeUriString($"api/rest/projects/{idProjeto}/subprojects/ ");

            var deletarSubProjetoRequest = restManager.PerformDeleteRequest(urlDeletarSubProjeto);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarSubProjetoRequest.StatusCode.Should().Be(400);
                    deletarSubProjetoRequest.StatusDescription.Should().Be(mensagemEsperada);
                    deletarSubProjetoRequest.Content.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(deletarSubProjetoRequest);
        }
    }
}
