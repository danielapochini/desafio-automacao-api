using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Response.Filters;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using DesafioAutomacaoAPI.Utils.Queries.Filters;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Filters
{
    public class ObterFiltroTest : IAssemblyFixture<TestBase>
    {
        private readonly RestManager restManager = new RestManager();

        [AllureXunit] 
        public void ObterFiltroValorExistente()
        {
            int filtroId = FiltersQueries.ListarUltimoFiltroPublicoCadastrado().Id;
            string urlObterFiltro = $"api/rest/filters/{filtroId}";

            var obterFiltroRequest = restManager.PerformGetRequest<FiltersResponse>(urlObterFiltro);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    obterFiltroRequest.StatusCode.Should().Be(200);
                    obterFiltroRequest.Data.Filters.Should().Contain(x => x.Id == filtroId && x.Name == "Filtro Pesquisa" && x.Public == true);
                    obterFiltroRequest.Data.Filters.Should().Contain(x => x.Project.Id == 0 && x.Project.Name == "All Projects");
                    obterFiltroRequest.Data.Filters.Should().Contain(x => x.Criteria.Status.Any(x => x.Id == 10 && x.Name == "new"));
                    obterFiltroRequest.Data.Filters.Should().Contain(x => x.Criteria.HideStatus.Id == 90 && x.Criteria.HideStatus.Name == "closed");
                    obterFiltroRequest.Data.Filters.Should().Contain(x => x.Criteria.Priority.Any(x => x.Id == 40 && x.Name == "high"));
                    obterFiltroRequest.Data.Filters.Should().Contain(x => x.Url == "http://localhost:8989/search.php?project_id=0&status=10&priority=40&sticky=on&sort=last_updated&dir=DESC&hide_status=90&match_type=0");
                }
            });

            AllureHelper.AdicionarResultado(obterFiltroRequest);
        }

        [AllureXunit]
        public void ObterFiltroValorInexistente()
        {
            int filtroId = FiltersQueries.ListarUltimoFiltroPublicoCadastrado().Id + DadosFakeHelper.GerarId();
            string urlObterFiltro = $"api/rest/filters/{filtroId}";

            var obterFiltroRequest = restManager.PerformGetRequest<FiltersResponse>(urlObterFiltro);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    obterFiltroRequest.StatusCode.Should().Be(200);
                    obterFiltroRequest.Data.Filters.Should().BeEmpty();
                }
            });

            AllureHelper.AdicionarResultado(obterFiltroRequest);
        }

        [AllureXunit]
        public void ObterTodosOsFiltrosCadastrados()
        { 
            string urlObterFiltro = $"api/rest/filters";

            var obterFiltroRequest = restManager.PerformGetRequest<FiltersResponse>(urlObterFiltro);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    obterFiltroRequest.StatusCode.Should().Be(200);
                    obterFiltroRequest.Data.Filters.Should().Contain(x => x.Id == 2 && x.Name == "Filtro Pesquisa" && x.Public == true);
                    obterFiltroRequest.Data.Filters.Should().Contain(x => x.Id == 3 && x.Name == "Filtro Teste" && x.Public == false); 
                }
            });

            AllureHelper.AdicionarResultado(obterFiltroRequest);
        }
    }
}
