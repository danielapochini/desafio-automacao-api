using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model;
using DesafioAutomacaoAPI.Utils;
using DesafioAutomacaoAPI.Utils.Helpers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Pages
{
    public class ObterPaginaIssuesTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Páginas";
        private const string subSuiteProjeto = "Obter Páginas Issues Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#ddd095f8-0905-ae9d-268b-a24cddfa8740";

        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Teste com valor de página inválido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void ObterPaginaIssuesValorInexistente()
        {
            int issueId = DadosFakeHelper.GerarId();
            string mensagemEsperada = $"Issue #{issueId} not found";
            string urlPaginasIssues = $"api/rest/issues/{issueId}";

            var ObterPaginaIssuesRequest = restManager.PerformGetRequest<ErrorMessageResponse>(urlPaginasIssues);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterPaginaIssuesRequest.StatusCode.Should().Be(404);
                    ObterPaginaIssuesRequest.StatusDescription.Should().Be(mensagemEsperada);
                    ObterPaginaIssuesRequest.Data.Message.Should().Be(mensagemEsperada);
                    ObterPaginaIssuesRequest.Data.Localized.Should().Be(string.Concat(mensagemEsperada.Replace("#", string.Empty),"."));
                    ObterPaginaIssuesRequest.Data.Code.Should().Be(1100);
                }
            });

            AllureHelper.AdicionarResultado(ObterPaginaIssuesRequest);
        }
    }
}
