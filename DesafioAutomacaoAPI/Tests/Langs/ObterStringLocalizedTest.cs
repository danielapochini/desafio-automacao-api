using Allure.Xunit;
using Allure.Xunit.Attributes;
using DesafioAutomacaoAPI.Base;
using DesafioAutomacaoAPI.Model.Response.Langs;
using DesafioAutomacaoAPI.Utils.Helpers;
using FluentAssertions;
using FluentAssertions.Execution; 
using Xunit.Extensions.AssemblyFixture;

namespace DesafioAutomacaoAPI.Tests.Langs
{ 
    public class ObterStringLocalizedTest : IAssemblyFixture<TestBase>
    {
        private const string suiteProjeto = "Linguagem";
        private const string subSuiteProjeto = "Obter String Localized Test";
        private const string linkDocumentacao = "https://documenter.getpostman.com/view/29959/mantis-bug-tracker-rest-api/7Lt6zkP#ddd095f8-0905-ae9d-268b-a24cddfa8740";

        private const string parametroStringValido = "all_projects";
        private const string parametroStringInvalido = "does_not_exist";

        private readonly RestManager restManager = new RestManager();

        [AllureXunit]
        [AllureDescription("Teste com valor válido")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Sucesso")]
        [AllureLink(linkDocumentacao)]
        public void ObterStringLocalizedValorValido()
        { 
            string urlObterString = $"api/rest/lang?string={parametroStringValido}";


            var ObterStringLocalizedRequest = restManager.PerformGetRequest<StringLocalizedResponse>(urlObterString);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterStringLocalizedRequest.StatusCode.Should().Be(200);
                    ObterStringLocalizedRequest.Data.Strings.Should().Contain(x => x.Name == "all_projects" && x.Localized == "All Projects");
                    ObterStringLocalizedRequest.Data.Language.Should().Be("english");
                }
            });

            AllureHelper.AdicionarResultado(ObterStringLocalizedRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com valor inexistente")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Cenário de Exceção")]
        [AllureLink(linkDocumentacao)]
        public void ObterStringLocalizedValorInexistente()
        { 
            string urlObterString = $"api/rest/lang?string={parametroStringInvalido}";

            var ObterStringLocalizedRequest = restManager.PerformGetRequest<StringLocalizedResponse>(urlObterString);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterStringLocalizedRequest.StatusCode.Should().Be(200);
                    ObterStringLocalizedRequest.Data.Strings.Should().BeEmpty();
                    ObterStringLocalizedRequest.Data.Language.Should().Be("english");
                }
            });

            AllureHelper.AdicionarResultado(ObterStringLocalizedRequest);
        }

        [AllureXunit]
        [AllureDescription("Teste com múltiplos valores")]
        [AllureSuite(suiteProjeto), AllureSubSuite(subSuiteProjeto), AllureTag("Teste Exploratório")]
        [AllureLink(linkDocumentacao)]
        public void ObterStringLocalizedMultiplosValores()
        {
            string parametroStringValidoDois = "move_bugs";
            string urlObterString = $"api/rest/lang?string[]={parametroStringValido}&string[]={parametroStringInvalido}&string[]={parametroStringValidoDois}";


            var ObterStringLocalizedRequest = restManager.PerformGetRequest<StringLocalizedResponse>(urlObterString);

            Steps.Step("Assertions", () =>
            {
                using (new AssertionScope())
                {
                    ObterStringLocalizedRequest.StatusCode.Should().Be(200);
                    ObterStringLocalizedRequest.Data.Strings.Should().Contain(x => x.Name == parametroStringValido && x.Localized == "All Projects");
                    ObterStringLocalizedRequest.Data.Strings.Should().Contain(x => x.Name == parametroStringValidoDois && x.Localized == "Move Issues");
                    ObterStringLocalizedRequest.Data.Strings.Should().NotContain(x => x.Name == parametroStringInvalido);
                    ObterStringLocalizedRequest.Data.Language.Should().Be("english");
                }
            });

            AllureHelper.AdicionarResultado(ObterStringLocalizedRequest);
        }
    }
}
