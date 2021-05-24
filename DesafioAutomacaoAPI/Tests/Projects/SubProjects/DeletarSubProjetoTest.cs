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
        private readonly SubProjectsEntities informacoesBD = SubProjectsQueries.ListarUltimoSubProjetoCadastrado();

        private static readonly RestManager restManager = new RestManager();

        [AllureXunit]
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
