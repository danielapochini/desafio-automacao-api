using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Filters;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Filters
{ 
    public class DeletarFiltroTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Filtros";
        private const string subSuiteProjeto = "Deletar Filtros Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#ddfb619e-f9c4-d8fd-10ee-0100213b0423";

        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Deletar filtro com valor válido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void DeletarFiltroValorExistente()
        {
            int filtroId = FiltersQueries.ListarUltimoFiltroCadastrado().Id;
            string urlDeletarFiltro = $"api/rest/filters/{filtroId}";

            var deletarFiltroRequest = restManager.PerformDeleteRequest(urlDeletarFiltro);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarFiltroRequest.StatusCode.Should().Be(204);
                    deletarFiltroRequest.Content.Should().BeEmpty(); 
                }
            });

            AllureHelper.AdicionarResultado(deletarFiltroRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com filtro inexistente")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void DeletarFiltroValorInexistente()
        {
            int filtroId = DadosFakeHelper.GerarId();
            string urlDeletarFiltro = $"api/rest/filters/{filtroId}";

            var deletarFiltroRequest = restManager.PerformDeleteRequest(urlDeletarFiltro);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    deletarFiltroRequest.StatusCode.Should().Be(404);
                    deletarFiltroRequest.Content.Should().BeEmpty();
                    deletarFiltroRequest.StatusDescription.Should().Be("Filter not found");
                }
            });

            AllureHelper.AdicionarResultado(deletarFiltroRequest);
        }

     
    }
}
