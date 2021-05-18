using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Filters;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Filters
{
    public class DeletarFiltroTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
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
